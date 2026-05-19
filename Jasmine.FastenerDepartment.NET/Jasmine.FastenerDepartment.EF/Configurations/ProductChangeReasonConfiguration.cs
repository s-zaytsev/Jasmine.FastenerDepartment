using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Jasmine.FastenerDepartment.EF.Configurations;

internal class ProductChangeReasonConfiguration : IEntityTypeConfiguration<ProductChangeReason>
{
    public void Configure(EntityTypeBuilder<ProductChangeReason> builder)
    {
        builder.ToTable("ProductChangeReasons");

        builder.HasKey(x => x.Id);

        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };

        builder
            .Property(x => x.Description)
            .HasColumnType("jsonb")
            .HasConversion(
                x => JsonSerializer.Serialize(x, options),
                x => JsonSerializer.Deserialize<LocalizedString>(x, options)
            )
            .IsRequired();
    }
}
