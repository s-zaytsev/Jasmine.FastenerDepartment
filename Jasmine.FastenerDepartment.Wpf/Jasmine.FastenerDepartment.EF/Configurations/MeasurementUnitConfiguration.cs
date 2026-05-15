using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.Configurations;

class MeasurementUnitConfiguration : IEntityTypeConfiguration<MeasurementUnit>
{
    public void Configure(EntityTypeBuilder<MeasurementUnit> builder)
    {
        builder.ToTable("MeasurementUnits");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ShortName).IsRequired();
        builder.Property(x => x.Name).IsRequired();
    }
}
