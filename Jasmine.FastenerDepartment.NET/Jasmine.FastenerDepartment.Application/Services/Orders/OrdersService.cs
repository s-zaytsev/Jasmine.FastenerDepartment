using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Common.Services;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.Messaging.Models;
using Jasmine.FastenerDepartment.Domain.Messaging.Services;
using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Jasmine.FastenerDepartment.Domain.Orders.Repositories;
using Jasmine.FastenerDepartment.Domain.Orders.Services;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.Domain.Products.Repositories;
using Jasmine.FastenerDepartment.Domain.Suppliers.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Repositories;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Services;
using Jasmine.FastenerDepartment.EF.Repositories.UnitOfWork;
using System.IO.Compression;

namespace Jasmine.FastenerDepartment.Application.Services.Orders;

internal class OrdersService : IOrdersService
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IProductsRepository _productsRepository;
    private readonly ISupplierProductsRepository _supplierProductsRepository;
    private readonly ITemplateRenderService _templateRenderService;
    private readonly IMessagesService _messageService;
    private readonly ILanguageService _languageService;
    private readonly IUnitOfWork _unitOfWork;

    public OrdersService(
        IOrdersRepository ordersRepository,
        IProductsRepository productsRepository,
        ISupplierProductsRepository supplierProductsRepository,
        ITemplateRenderService templateRenderService,
        IMessagesService messageService,
        ILanguageService languageService,
        IUnitOfWork unitOfWork)
    {
        _ordersRepository = ordersRepository;
        _productsRepository = productsRepository;
        _supplierProductsRepository = supplierProductsRepository;
        _templateRenderService = templateRenderService;
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

        var messageTitle = GetOrderTitle(order);
        var renderedFiles = await RenderFilesAsync(model.Formats, order, model.TemplateId, model.CompanyId);
        var messageRequest = CreateMessageRequest(model, order, messageTitle, renderedFiles);

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

    public async Task<FileModel> DownloadOrderFilesAsZipAsync(
        Guid id, DownloadOrderModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);

        var order = await GetByIdAsync(id, cancellationToken);
        
        var fileName = GetOrderTitle(order);
        var renderedFiles = await RenderFilesAsync(model.Formats, order, model.TemplateId, model.CompanyId);

        var zipMemoryStream = new MemoryStream();

        using (var archive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Create, leaveOpen: true))
        {
            foreach (var file in renderedFiles)
            {
                var entry = archive.CreateEntry(file.Name);
                using var entryStream = entry.Open();
                file.Content.Position = 0;
                file.Content.CopyTo(entryStream);
            }

            foreach (var file in model.Attachments)
            {
                var entry = archive.CreateEntry(file.Name);
                using var entryStream = entry.Open();
                file.Content.Position = 0;
                file.Content.CopyTo(entryStream);
            }
        }

        zipMemoryStream.Position = 0;

        var zipFile = new FileModel
        {
            Name = $"{fileName}.zip",
            Content = zipMemoryStream,
            ContentType = "application/zip"
        };

        return zipFile;
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

    private string GetOrderTitle(Order order)
    {
        return _languageService.LanguageCode switch
        {
            LanguageCode.Russian => $"Заказ #{order.GetNumberAsText()}",
            LanguageCode.English => $"Order #{order.GetNumberAsText()}",
            _ => throw new NotSupportedException($"Language {_languageService.LanguageCode} not supported.")
        };
    }

    private async Task<List<FileModel>> RenderFilesAsync(
        ICollection<TemplateFormatCode> formatCodes,
        Order order,
        Guid templateId,
        Guid? companyId)
    {
        var renders = new List<FileModel>();

        foreach (var format in formatCodes)
        {
            var render = await _templateRenderService.RenderOrderFormAsync(new()
            {
                OrderId = order.Id,
                TemplateId = templateId,
                CompanyId = companyId,
                FormatCode = format
            });

            renders.Add(render.AsFile());
        }

        return renders;
    }

    private MessageRequest<OrderFormMessageRequestData> CreateMessageRequest(
        SendOrderModel model, Order order, string title, List<FileModel> renderedFiles)
    {
        var messageRequest = new MessageRequest<OrderFormMessageRequestData>
        {
            MessageTypeCode = model.MessageType,
            TemplateTypeCode = TemplateTypeCode.OrderForm,
            RecipientContact = model.RecipientContact,
            Title = title,
            Attachments = renderedFiles,
            Data = new(
                order,
                renderedFiles.Count + model.Attachments.Count > 0,
                _languageService.LanguageCode),
        };

        messageRequest.Attachments.AddRange(model.Attachments);

        return messageRequest;
    }
}
