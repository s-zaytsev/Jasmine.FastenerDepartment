using Jasmine.FastenerDepartment.Application.Models.Synchronization;
using Jasmine.FastenerDepartment.Documents.Export.Models;
using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Common.Services;
using Jasmine.FastenerDepartment.Domain.HistoryEntries.Models;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.Domain.ProductsToOrder.Models;
using Jasmine.FastenerDepartment.Domain.ProductTypes.Models;
using Jasmine.FastenerDepartment.Domain.Settings.Models.Company;
using Jasmine.FastenerDepartment.Domain.Settings.Models.Emails;
using Jasmine.FastenerDepartment.Domain.Suppliers.Models;
using Jasmine.FastenerDepartment.WebApi.Dtos.Common;
using Jasmine.FastenerDepartment.WebApi.Dtos.Documents;
using Jasmine.FastenerDepartment.WebApi.Dtos.HistoryEntries;
using Jasmine.FastenerDepartment.WebApi.Dtos.Orders;
using Jasmine.FastenerDepartment.WebApi.Dtos.Products;
using Jasmine.FastenerDepartment.WebApi.Dtos.ProductsToOrder;
using Jasmine.FastenerDepartment.WebApi.Dtos.ProductTypes;
using Jasmine.FastenerDepartment.WebApi.Dtos.SettingsEntries;
using Jasmine.FastenerDepartment.WebApi.Dtos.Suppliers;
using Jasmine.FastenerDepartment.WebApi.Dtos.Synchronization;

namespace Jasmine.FastenerDepartment.WebApi;

/// <summary>
/// Web API mapper.
/// </summary>
public class WebApiMapper
{
    private readonly ILanguageService _languageService;

    /// <summary>
    /// Creates mapper.
    /// </summary>
    public WebApiMapper(ILanguageService languageService)
    {
        _languageService = languageService;
    }

    internal Quantity Map(QuantityDto model)
    {
        return new(model.Value, model.MeasurementUnitCode, model.SpecialMeasurementUnit);
    }

    internal QuantityDto Map(Quantity model)
    {
        if (model == null) return null;
        return new(model.Value, model.MeasurementUnitCode, model.SpecialMeasurementUnit);
    }

    internal ProductsQuery Map(ProductsQueryDto query)
    {
        return new ProductsQuery
        {
            PageNo = query.PageNo,
            PageSize = query.PageSize,
            Search = query.Search,
            SortDesc = query.SortDesc,
            SortBy = query.SortBy,
            IncludeDeleted = query.IncludeDeleted,
            OnlyToOrder = query.OnlyToOrder,
            OnlyToPrint = query.OnlyToPrint,
            PriceFrom = query.PriceFrom,
            PriceTo = query.PriceTo,
            PriceTags = query.PriceTags ?? [],
            Suppliers = query.Suppliers ?? [],
            Types = query.Types ?? [],
            LanguageCode = _languageService.LanguageCode
        };
    }

    internal ProductFiltersDto Map(ProductFilters filters)
    {
        return new ProductFiltersDto
        {
            OnlyToOrder = filters.OnlyToOrder,
            OnlyToPrint = filters.OnlyToPrint,
            PriceRange = filters.PriceRange,
            PriceTags = filters.PriceTags,
            Suppliers = filters.Suppliers,
            Types = filters.Types
        };
    }

    internal Page<ProductDto> Map(Page<Product> page)
    {
        return new Page<ProductDto>
        {
            PageNo = page.PageNo,
            PageSize = page.PageSize,
            TotalCount = page.TotalCount,
            TotalPages = page.TotalPages,
            Items = [.. page.Items.Select(Map)]
        };
    }

    internal ProductDto Map(Product product)
    {
        return new(
            product.Id,
            product.Number.Value,
            product.CreatedDate,
            product.ModifiedDate,
            product.Name.Value,
            product.Price.Value,
            product.HasHardwareSize,
            product.IsHardwareSizeEnabled,
            product.IsNeededToOrder,
            product.IsNeededToPrint,
            product.IsDeleted,
            product.MeasurementUnitCode,
            product.PriceTagCode,
            Map(product.Type),
            product.Suppliers?
                   .Select(Map)
                   .ToList(),
            product.HistoryEntries?
                   .Select(Map)
                   .OrderByDescending(x => x.CreatedDate)
                   .ToList());
    }

    internal ProductHistoryEntryDto Map(ProductHistoryEntry entry)
    {
        return new(
            entry.Id,
            entry.CreatedDate,
            entry.ProductId,
            Map(entry.Reason),
            entry.OldValue,
            entry.NewValue,
            entry.Product.Number.Value);
    }

    internal ProductChangeReasonDto Map(ProductChangeReason model)
    {
        if (model == null) return null;
        return new ProductChangeReasonDto(model.Id, model.Description.GetText(_languageService.LanguageCode));
    }

    internal ChangeProductModel Map(ChangeProductModelDto model)
    {
        return new ChangeProductModel
        {
            Name = model.Name,
            Price = model.Price,
            TypeId = model.TypeId,
            PriceTagCode = model.PriceTagCode,
            IsHardwareSizeEnabled = model.IsHardwareSizeEnabled,
            IsNeededToOrder = model.IsNeededToOrder,
            IsNeededToPrint = model.IsNeededToPrint,
            MeasurementUnitCode = model.MeasurementUnitCode,
            SupplierIds = model.SupplierIds
        };
    }

    internal MeasurementUnitDto Map(MeasurementUnit model)
    {
        if (model == null) return null;
        return new(
            model.Id,
            model.ShortName.GetText(_languageService.LanguageCode),
            model.Name.GetText(_languageService.LanguageCode));
    }

    internal ExportDocumentRequest Map(ExportDocumentRequestDto model)
    {
        return new()
        {
            DocumentType = model.DocumentType
        };
    }

    internal ExportDocumentResponseDto Map(ExportDocumentResponse model)
    {
        return new(model.Name, model.Stream);
    }

    internal SynchronizationRequest Map(SynchronizationRequestDto model)
    {
        return new()
        {
            LastSynchronizeUtcTime = model.LastSynchronizeUtcTime,
            Products = model.Products.Select(Map)
        };
    }

    internal SynchronizationResponseDto Map(SynchronizationResponse model)
    {
        return new(
            model.NewProducts.Select(Map),
            model.ModifiedProducts.Select(Map),
            model.HistoryEntries.Select(Map));
    }

    internal SynchronizationHistoryEntryDto Map(SynchronizationHistoryEntry model)
    {
        return new(model.Date, model.Items.Select(Map));
    }

    internal SynchronizationHistoryEntryItemDto Map(SynchronizationHistoryEntryItem model)
    {
        return new(model.ProductId, model.ProductHistoryEntries.Select(Map));
    }

    internal SynchronizationProduct Map(SynchronizationProductDto model)
    {
        return new()
        {
            Id = model.Id,
            CreatedDate = model.CreatedDate,
            ModifiedDate = model.ModifiedDate,
            Number = model.Number,
            Name = model.Name,
            Price = model.Price,
            PriceTagCode = model.PriceTagCode,
            MeasurementUnitCode = model.MeasurementUnitCode,
            IsDeleted = model.IsDeleted,
            IsNeededToOrder = model.IsNeededToOrder,
            IsNeededToPrint = model.IsNeededToPrint
        };
    }

    internal SynchronizationProductDto Map(SynchronizationProduct model)
    {
        return new(
            model.Id,
            model.Number,
            model.CreatedDate,
            model.ModifiedDate,
            model.Name,
            model.Price,
            model.IsDeleted,
            model.IsNeededToOrder,
            model.IsNeededToPrint,
            model.MeasurementUnitCode,
            model.PriceTagCode);
    }

    internal SupplierDto Map(Supplier supplier)
    {
        if (supplier == null) return null;
        return new(supplier.Id, supplier.Name.Value, supplier.Address);
    }

    internal ExtendedSupplierDto Map(ExtendedSupplier model)
    {
        return new(model.Id, model.Name.Value, model.Address, model.ProductCount, model.ActiveOrderCount);
    }

    internal ChangeSupplierModel Map(ChangeSupplierModelDto model)
    {
        return new(model.Name, model.Address);
    }

    internal SupplierProductsQuery Map(SupplierProductsQueryDto query)
    {
        return new SupplierProductsQuery
        {
            PageNo = query.PageNo,
            PageSize = query.PageSize,
            Search = query.Search,
            SortDesc = query.SortDesc,
            SortBy = query.SortBy,
            SupplierId = query.SupplierId
        };
    }

    internal Page<SupplierProductDto> Map(Page<SupplierProduct> page)
    {
        return new Page<SupplierProductDto>
        {
            PageNo = page.PageNo,
            PageSize = page.PageSize,
            TotalCount = page.TotalCount,
            TotalPages = page.TotalPages,
            Items = [.. page.Items.Select(Map)]
        };
    }

    internal SupplierProductDto Map(SupplierProduct model)
    {
        return new(model.Id, model.Number, Map(model.Product));
    }

    internal ChangeSupplierProductModel Map(ChangeSupplierProductModelDto model)
    {
        return new()
        {
            Number = model.Number
        };
    }

    internal OrdersQuery Map(OrdersQueryDto query)
    {
        return new OrdersQuery
        {
            PageNo = query.PageNo,
            PageSize = query.PageSize,
            Search = query.Search,
            SortDesc = query.SortDesc,
            SortBy = query.SortBy,
            OnlyCompleted = query.OnlyCompleted,
            OnlyActive = query.OnlyActive
        };
    }

    internal Page<OrderDto> Map(Page<Order> page)
    {
        return new()
        {
            PageNo = page.PageNo,
            PageSize = page.PageSize,
            TotalCount = page.TotalCount,
            TotalPages = page.TotalPages,
            Items = [.. page.Items.Select(Map)]
        };
    }

    internal OrderDto Map(Order order)
    {
        return new(
            order.Id,
            order.CreatedDate,
            order.Number,
            order.StatusCode,
            Map(order.Supplier),
            [.. order.Products.Select(Map)],
            [.. order.HistoryEntries.Select(Map)]);
    }

    internal OrderProductDto Map(OrderProduct model)
    {
        return new(
            model.Id,
            model.ProductId,
            model.ProductName.Value,
            model.Product?.Price.Value ?? 0,
            Map(model.Product?.Type),
            Map(model.Ordered),
            Map(model.Fulfilled),
            model.SupplierProductNumber,
            model.IsFulfilled);
    }

    internal OrderHistoryEntryDto Map(OrderHistoryEntry model)
    {
        return new(model.Id, model.CreatedDate, model.OrderStatusCode, model.Comment);
    }

    internal ChangeOrderModel Map(ChangeOrderDto model)
    {
        return new()
        {
            Products = [.. model.Products.Select(Map)]
        };
    }

    internal CreateOrderModel Map(CreateOrderDto model)
    {
        return new()
        {
            SupplierId = model.SupplierId,
            Products = [.. model.Products.Select(Map)]
        };
    }

    internal OrderProduct Map(ChangeOrderProductDto model)
    {
        return new(
            model.ProductId,
            model.ProductName,
            model.ProductTypeId,
            Map(model.Ordered),
            model.SupplierProductNumber);
    }

    internal CompleteOrderModel Map(CompleteOrderDto model)
    {
        return new()
        {
            Comment = model.Comment,
            Products = [.. model.Products.Select(Map)]
        };
    }

    internal CompleteOrderProduct Map(CompleteOrderProductDto model)
    {
        return new()
        {
            AddToPrint = model.AddToPrint,
            Fulfilled = Map(model.Fulfilled),
            IsFulfilled = model.IsFulfilled,
            OrderProductId = model.OrderProductId,
            Price = new(model.Price),
            ProductId = model.ProductId,
            ProductName = model.ProductName,
            ProductTypeId = model.ProductTypeId,
            SupplierProductNumber = model.SupplierProductNumber,
            RemoveOrderStatus = model.RemoveOrderStatus
        };
    }

    internal CancelOrderModel Map(CancelOrderDto model)
    {
        return new() { Comment = model.Comment };
    }

    internal SendOrderModel Map(SendOrderModelDto dto)
    {
        return new() 
        {
            RecipientContact = dto.RecipientContact,
            MessageType = dto.MessageType
        };
    }

    internal ProductTypeDto Map(ProductType model)
    {
        if (model == null) return null;
        return new(model.Id, model.Name.Value);
    }

    internal ExtendedProductTypeDto Map(ExtendedProductType model)
    {
        return new(model.Id, model.Name.Value, model.ProductCount);
    }

    internal ChangeProductType Map(ChangeProductTypeDto model)
    {
        return new()
        {
            Name = model.Name
        };
    }

    internal Page<ProductToOrderDto> Map(Page<ProductToOrder> page)
    {
        return new()
        {
            PageNo = page.PageNo,
            PageSize = page.PageSize,
            TotalCount = page.TotalCount,
            TotalPages = page.TotalPages,
            Items = [.. page.Items.Select(Map)]
        };
    }

    internal ProductToOrderDto Map(ProductToOrder model)
    {
        return new(Map(model.Product), [.. model.SupplierNumbers.Select(Map)]);
    }

    internal SupplierNumberDto Map(SupplierNumber model)
    {
        return new(model.SupplierId, model.Number);
    }

    internal ProductsToOrderQuery Map(ProductsToOrderQueryDto model)
    {
        return new()
        {
            Search = model.Search,
            SupplierId = model.SupplierId,
            OnlyToOrder = model.OnlyToOrder
        };
    }

    internal CompanySettingsDto Map(CompanySettings model)
    {
        return new(model.Title, model.SubTitle);
    }

    internal EmailSettingsDto Map(EmailSettings model)
    {
        return new(
            model.SmtpUrl,
            model.SmtpPort,
            model.UserName,
            model.Password,
            model.DisplayName);
    }

    internal ChangeCompanySettings Map(ChangeCompanySettingsDto dto)
    {
        return new()
        {
            Title = dto.Title,
            SubTitle = dto.SubTitle
        };
    }

    internal ChangeEmailSettings Map(ChangeEmailSettingsDto dto)
    {
        return new()
        {
            SmtpUrl = dto.SmtpUrl,
            SmtpPort = dto.SmtpPort,
            UserName = dto.UserName,
            Password = dto.Password,
            DisplayName = dto.DisplayName
        };
    }
}
