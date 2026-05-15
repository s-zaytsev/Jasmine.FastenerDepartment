using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;

/// <summary>
/// Measurement unit.
/// </summary>
public class MeasurementUnit : EntityBase<MeasurementUnitCode>
{
    /// <summary>
    /// Short name. 
    /// </summary>
    public string ShortName { get; init; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; init; }

    private MeasurementUnit() { }

    /// <summary>
    /// Creates measurement unit.
    /// </summary>
    public MeasurementUnit(
        MeasurementUnitCode id,
        string shortName,
        string name)
    {
        Id = id;
        ShortName = shortName;
        Name = name;
    }
}
