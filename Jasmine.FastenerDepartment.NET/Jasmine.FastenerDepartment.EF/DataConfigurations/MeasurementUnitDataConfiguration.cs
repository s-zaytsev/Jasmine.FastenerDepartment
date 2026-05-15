using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.DataConfigurations;

class MeasurementUnitDataConfiguration : IEntityTypeConfiguration<MeasurementUnit>
{
    public void Configure(EntityTypeBuilder<MeasurementUnit> builder)
    {
        builder.HasData(
            Create(MeasurementUnitCode.Pieces, "pcs", "Pieces"),
            Create(MeasurementUnitCode.Meters, "m", "Meters"),
            Create(MeasurementUnitCode.Kilograms, "kg", "Kilograms"),
            Create(MeasurementUnitCode.Packages, "pack", "Packages"),
            Create(MeasurementUnitCode.Sets, "sets", "Sets"),
            Create(MeasurementUnitCode.Lists, "l", "Lists"));
    }

    private MeasurementUnit Create(MeasurementUnitCode id, string shortName, string name)
    {
        return new MeasurementUnit(id, shortName, name);
    }
}
