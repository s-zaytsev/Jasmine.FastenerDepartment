using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Products;

/// <summary>
/// Change product model.
/// </summary>
/// <param name="Name">Name.</param>
/// <param name="Price">Price.</param>
/// <param name="TypeId">Product type identifier.</param>
/// <param name="IsHardwareSizeEnabled">Is hardware size enabled.</param>
/// <param name="IsNeededToOrder">Is needed to order.</param>
/// <param name="IsNeededToPrint">Is needed to print.</param>
/// <param name="PriceTagCode">Price tag code.</param>
/// <param name="MeasurementUnitCode">Measurement unit code.</param>
/// <param name="SupplierIds">List of supplier identifiers.</param>
public record ChangeProductModelDto (
    string Name,
    decimal Price,
    Guid? TypeId,
    bool IsHardwareSizeEnabled,
    bool IsNeededToOrder,
    bool IsNeededToPrint,
    PriceTagCode PriceTagCode,
    MeasurementUnitCode MeasurementUnitCode,
    ICollection<Guid> SupplierIds);
