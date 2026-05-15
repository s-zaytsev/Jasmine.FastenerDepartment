using Jasmine.FastenerDepartment.Domain.Common.Repositories;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;

namespace Jasmine.FastenerDepartment.Domain.MeasurementUnits.Repositories;

/// <summary>
/// Measurement units repository.
/// </summary>
public interface IMeasurementUnitsRepository :
    IEntitiesRepository<MeasurementUnitCode, MeasurementUnit>
{}
