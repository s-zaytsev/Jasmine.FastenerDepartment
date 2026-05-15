using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Common;

/// <summary>
/// Quantity.
/// </summary>
/// <param name="Value">Value.</param>
/// <param name="MeasurementUnitCode">Measurement unit code.</param>
/// <param name="SpecialMeasurementUnit">Special measurement unit.</param>
public record QuantityDto(
    double Value,
    MeasurementUnitCode? MeasurementUnitCode,
    string SpecialMeasurementUnit);
