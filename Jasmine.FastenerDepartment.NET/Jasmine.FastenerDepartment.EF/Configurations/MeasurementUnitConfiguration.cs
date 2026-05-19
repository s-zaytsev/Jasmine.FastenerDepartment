using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Jasmine.FastenerDepartment.EF.Configurations;

class MeasurementUnitConfiguration : IEntityTypeConfiguration<MeasurementUnit>
{
    public void Configure(EntityTypeBuilder<MeasurementUnit> builder)
    {
        builder.ToTable("MeasurementUnits");

        builder.HasKey(x => x.Id);

        var options = new JsonSerializerOptions 
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };

        builder
            .Property(x => x.ShortName)
            .HasColumnType("jsonb")
            .HasConversion(
                x => JsonSerializer.Serialize(x, options),
                x => JsonSerializer.Deserialize<LocalizedString>(x, options)
            )
            .IsRequired();

        builder
            .Property(x => x.Name)
            .HasColumnType("jsonb")
            .HasConversion(
                x => JsonSerializer.Serialize(x, options),
                x => JsonSerializer.Deserialize<LocalizedString>(x, options)
            )
            .IsRequired();
    }
}
