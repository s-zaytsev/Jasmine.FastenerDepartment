using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Products;

/// <summary>
/// Measurement unit.
/// </summary>
/// <param name="Id">Identifier.</param>
/// <param name="ShortName">Short name.</param>
/// <param name="Name">Name.</param>
public record MeasurementUnitDto(
    MeasurementUnitCode Id,
    string ShortName,
    string Name);
