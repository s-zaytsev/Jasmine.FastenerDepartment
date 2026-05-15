using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;
using Jasmine.FastenerDepartment.WebApi.Dtos.HistoryEntries;
using Jasmine.FastenerDepartment.WebApi.Dtos.ProductTypes;
using Jasmine.FastenerDepartment.WebApi.Dtos.Suppliers;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Products;

/// <summary>
/// Product.
/// </summary>
/// <param name="Id">Identifier.</param>
/// <param name="Number">Number.</param>
/// <param name="CreatedDate">Created date.</param>
/// <param name="ModifiedDate">Modified date.</param>
/// <param name="Name">Name.</param>
/// <param name="Price">Price.</param>
/// <param name="HasHardwareSize">Has hardware size.</param>
/// <param name="IsHardwareSizeEnabled">Is hardware size enabled.</param>
/// <param name="IsNeededToOrder">Is needed to order.</param>
/// <param name="IsNeededToPrint">Is needed to print.</param>
/// <param name="IsDeleted">IS deleted.</param>
/// <param name="MeasurementUnitCode">Measurement unit code.</param>
/// <param name="PriceTagCode">Price tag code.</param>
/// <param name="Type">Type.</param>
/// <param name="Suppliers">Suppliers.</param>
/// <param name="HistoryEntries">History entries.</param>
public record ProductDto(
    Guid Id,
    int Number,
    DateTime CreatedDate,
    DateTime ModifiedDate,
    string Name,
    decimal Price,
    bool HasHardwareSize,
    bool IsHardwareSizeEnabled,
    bool IsNeededToOrder,
    bool IsNeededToPrint,
    bool IsDeleted,
    MeasurementUnitCode MeasurementUnitCode,
    PriceTagCode PriceTagCode,
    ProductTypeDto Type,
    ICollection<SupplierDto> Suppliers,
    ICollection<ProductHistoryEntryDto> HistoryEntries);
