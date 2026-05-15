using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Synchronization;

/// <summary>
/// Synchronization product.
/// </summary>
/// <param name="Id">Identifier.</param>
/// <param name="Number">Number.</param>
/// <param name="CreatedDate">Created date.</param>
/// <param name="ModifiedDate">Modified date.</param>
/// <param name="Name">Name.</param>
/// <param name="Price">Price.</param>
/// <param name="IsDeleted">Is deleted.</param>
/// <param name="IsNeededToOrder">Is needed to order.</param>
/// <param name="IsNeededToPrint">Is needed to print.</param>
/// <param name="MeasurementUnitCode">Measurement unit code.</param>
/// <param name="PriceTagCode">Price tag code.</param>
public record SynchronizationProductDto(
    Guid Id,
    int Number,
    DateTime CreatedDate,
    DateTime ModifiedDate,
    string Name,
    decimal Price,
    bool IsDeleted,
    bool IsNeededToOrder,
    bool IsNeededToPrint,
    MeasurementUnitCode MeasurementUnitCode,
    PriceTagCode PriceTagCode);
