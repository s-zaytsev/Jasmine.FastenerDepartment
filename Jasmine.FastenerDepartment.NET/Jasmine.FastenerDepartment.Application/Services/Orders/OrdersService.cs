using Jasmine.FastenerDepartment.Documents.Orders.Services;
using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Common.Services;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Jasmine.FastenerDepartment.Domain.Orders.Repositories;
using Jasmine.FastenerDepartment.Domain.Orders.Services;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.Domain.Products.Repositories;
using Jasmine.FastenerDepartment.Domain.Suppliers.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Repositories;
using Jasmine.FastenerDepartment.EF.Repositories.UnitOfWork;
using Jasmine.FastenerDepartment.Messaging.Models;
using Jasmine.FastenerDepartment.Messaging.Services;
using Jasmine.FastenerDepartment.Templates.Models;
using Jasmine.FastenerDepartment.Templates.Services;

namespace Jasmine.FastenerDepartment.Application.Services.Orders;

internal class OrdersService : IOrdersService
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IProductsRepository _productsRepository;
    private readonly ISupplierProductsRepository _supplierProductsRepository;
    private readonly IOrderDocumentsService _orderDocumentsService;
    private readonly ITemplateService _templateService;
    private readonly IMessageService _messageService;
    private readonly ILanguageService _languageService;
    private readonly IUnitOfWork _unitOfWork;

    public OrdersService(
        IOrdersRepository ordersRepository,
        IProductsRepository productsRepository,
        ISupplierProductsRepository supplierProductsRepository,
        IOrderDocumentsService orderDocumentsService,
        ITemplateService templateService,
        IMessageService messageService,
        ILanguageService languageService,
        IUnitOfWork unitOfWork)
    {
        _ordersRepository = ordersRepository;
        _productsRepository = productsRepository;
        _supplierProductsRepository = supplierProductsRepository;
        _orderDocumentsService = orderDocumentsService;
        _templateService = templateService;
        _messageService = messageService;
        _languageService = languageService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Page<Order>> GetPageAsync(OrdersQuery query, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        var page = await _ordersRepository.GetPageAsync<OrdersQuery, OrdersQueryParameter>(query, cancellationToken);
        return page;
    }

    public async Task<Order> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await GetByIdAsync(id, cancellationToken);
        return order;
    }

    public async Task<Order> CreateAsync(CreateOrderModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);

        if (model.Products is not null && model.Products.Count == 0)
        {
            throw new InvalidOperationException("Order product list can't be empty.");
        }

        var lastOrderNumber = await _ordersRepository.GetLastNumberAsync(cancellationToken);

        var order = new Order(++lastOrderNumber, model.SupplierId, model.Products);

        _ordersRepository.Add(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order;
    }

    public async Task UpdateAsync(Guid id, ChangeOrderModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);

        if (model.Products is not null && model.Products.Count == 0)
        {
            throw new InvalidOperationException("Order product list can't be empty.");
        }

        var order = await GetByIdAsync(id, cancellationToken);
        order.ChangeProducts(model.Products);

        _ordersRepository.Update(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task SendAsync(Guid id, SendOrderModel model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(model);

        var order = await GetByIdAsync(id, cancellationToken);

        if (order.StatusCode == OrderStatusCode.Fulfilled)
            throw new InvalidOperationException("Order is already fulfilled.");

        if (order.StatusCode == OrderStatusCode.Cancelled)
            throw new InvalidOperationException("Order is already cancelled.");

        var templateType = GetTemplateType(model.MessageType);
        var template = _templateService.GetOrderRequestTemplate(templateType, order);
        var messageTitle = GetOrderTitle(order);

        var messageRequest = new MessageRequest
        {
            Content = template,
            RecipientContact = model.RecipientContact,
            Type = model.MessageType,
            Title = messageTitle
        };

        await _messageService.SendAsync(messageRequest, cancellationToken);

        order.ChangeStatus(OrderStatusCode.Sent, model.RecipientContact);

        _ordersRepository.Update(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task CompleteAsync(Guid id, CompleteOrderModel model, CancellationToken cancellationToken = default)
    {
        var order = await GetByIdAsync(id, cancellationToken);

        if (order.StatusCode == OrderStatusCode.Fulfilled || order.StatusCode == OrderStatusCode.Cancelled)
        {
            throw new InvalidOperationException("Order is already completed.");
        }

        AddExtraProducts(order, model);

        var lastProductNumber = await _productsRepository.GetLastProductNumberAsync(cancellationToken);

        ProcessNewProducts(order, model, lastProductNumber);

        var existedProductIds = order.Products.Where(x => x.ProductId.HasValue).Select(x => x.ProductId).Cast<Guid>();

        var products = await _productsRepository.GetByIdsAsync(existedProductIds, cancellationToken);

        ProcessExistingProducts(order, model, products);

        await ProcessSupplierProductsAsync(order, cancellationToken);

        order.ChangeStatus(OrderStatusCode.Fulfilled);
        _ordersRepository.Update(order);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task CancelAsync(Guid id, CancelOrderModel model, CancellationToken cancellationToken = default)
    {
        var order = await GetByIdAsync(id, cancellationToken);

        order.ChangeStatus(OrderStatusCode.Cancelled, model.Comment);

        _ordersRepository.Update(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<FileStreamModel> GetOrderDocumentStreamAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await GetByIdAsync(id, cancellationToken);
        var stream = await _orderDocumentsService.GetStreamAsync(order);

        return new FileStreamModel { Name = order.Number.ToString(), Stream = stream };
    }

    private async Task<Order> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await _ordersRepository.GetByIdAsync(id, cancellationToken);
        if (order == null)
        {
            throw new InvalidOperationException($"Order {id} not found.");
        }

        return order;
    }

    private void AddExtraProducts(Order order, CompleteOrderModel model)
    {
        var extraProducts = model
            .Products
            .Where(x => !x.OrderProductId.HasValue)
            .Select(x => new OrderProduct(
                x.ProductId,
                x.ProductName,
                x.ProductTypeId,
                new(0, x.Fulfilled.MeasurementUnitCode, x.Fulfilled.SpecialMeasurementUnit),
                x.SupplierProductNumber));

        if (!extraProducts.Any())
        {
            return;
        }

        var orderProducts = order.Products.Union(extraProducts);
        order.ChangeProducts([.. orderProducts]);
    }

    private void ProcessNewProducts(Order order, CompleteOrderModel model, int lastProductNumber)
    {
        foreach (var orderProduct in order.Products)
        {
            var completeOrderProduct = model.Products.First(y => y.ProductName == orderProduct.ProductName.Value);

            if (!completeOrderProduct.IsFulfilled)
            {
                continue;
            }

            if (orderProduct.ProductId.HasValue)
            {
                continue;
            }

            var product = new Product(
                ++lastProductNumber,
                orderProduct.ProductName.Value,
                completeOrderProduct.Price.Value,
                PriceTagCode.M,
                orderProduct.Ordered.MeasurementUnitCode ?? MeasurementUnitCode.Pieces,
                orderProduct.ProductTypeId,
                false,
                !completeOrderProduct.RemoveOrderStatus,
                completeOrderProduct.AddToPrint);

            _productsRepository.Add(product);

            orderProduct.SetFulfilledStatus();
            orderProduct.ChangeFulfilledQuantity(completeOrderProduct.Fulfilled);
            orderProduct.ChangeSupplierProductNumber(completeOrderProduct.SupplierProductNumber);

            orderProduct.SetProductId(product.Id);
        }
    }

    private void ProcessExistingProducts(Order order, CompleteOrderModel model, ICollection<Product> products)
    {
        foreach (var product in products)
        {
            var completeOrderProduct =
                model.Products.First(x => x.ProductId.HasValue && x.ProductId.Value == product.Id);

            if (!completeOrderProduct.IsFulfilled)
            {
                continue;
            }

            if (completeOrderProduct.AddToPrint)
            {
                product.ChangePrintStatus(true);
            }

            if (completeOrderProduct.RemoveOrderStatus)
            {
                product.ChangeOrderStatus(false);
            }

            product.ChangePrice(completeOrderProduct.Price.Value);

            var orderProduct = order.Products.First(x => x.ProductId == product.Id);
            orderProduct.SetFulfilledStatus();
            orderProduct.ChangeFulfilledQuantity(completeOrderProduct.Fulfilled);
            orderProduct.ChangeSupplierProductNumber(completeOrderProduct.SupplierProductNumber);
        }

        _productsRepository.UpdateRange(products);
    }

    private async Task ProcessSupplierProductsAsync(Order order, CancellationToken cancellationToken)
    {
        if (!order.SupplierId.HasValue)
        {
            return;
        }

        var fulfilledProducts = order.Products.Where(x => x.IsFulfilled);

        foreach (var orderProduct in fulfilledProducts)
        {
            var supplierProduct = await _supplierProductsRepository
                   .GetByProductIdAsync(orderProduct.Id, order.SupplierId.Value, cancellationToken);

            if (supplierProduct != null)
            {
                UpdateSupplierProduct(supplierProduct, orderProduct);
            }
            else
            {
                AddSupplierProduct(order.SupplierId.Value, orderProduct);
            }
        }
    }

    private void AddSupplierProduct(Guid supplierId, OrderProduct orderProduct)
    {
        var supplierProduct = new SupplierProduct(
            supplierId,
            orderProduct.ProductId.Value,
            orderProduct.SupplierProductNumber);

        _supplierProductsRepository.Add(supplierProduct);
    }

    private void UpdateSupplierProduct(SupplierProduct supplierProduct, OrderProduct orderProduct)
    {
        supplierProduct.ChangeNumber(orderProduct.SupplierProductNumber);
        _supplierProductsRepository.Update(supplierProduct);
    }

    private TemplateType GetTemplateType(MessageType messageType)
    {
        return messageType switch
        {
            MessageType.Email => TemplateType.Html,
            _ => throw new NotSupportedException($"Message type {messageType} not supported.")
        };
    }

    private string GetOrderTitle(Order order)
    {
        return _languageService.LanguageCode switch
        {
            LanguageCode.Russian => $"Заказ #{order.GetNumberAsText()}",
            LanguageCode.English => $"Order #{order.GetNumberAsText()}",
            _ => throw new NotSupportedException($"Language {_languageService.LanguageCode} not supported.")
        };
    }
}
