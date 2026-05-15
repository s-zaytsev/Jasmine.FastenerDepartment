using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Repositories;
using Jasmine.FastenerDepartment.EF.Repositories.Common;

namespace Jasmine.FastenerDepartment.EF.Repositories.MeasurementUnits;

/// <summary>
/// Measurement units repository.
/// </summary>
internal class MeasurementUnitsRepository
    : EntitiesRepositoryBase<MeasurementUnitCode, MeasurementUnit>, IMeasurementUnitsRepository
{
    /// <summary>
    /// Creates repository.
    /// </summary>
    /// <param name="context">Context.</param>
    public MeasurementUnitsRepository(ApplicationDbContext context)
        : base(context)
    { }
}
