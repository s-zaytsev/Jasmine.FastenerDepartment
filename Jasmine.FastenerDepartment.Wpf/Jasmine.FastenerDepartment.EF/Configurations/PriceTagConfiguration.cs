using Jasmine.FastenerDepartment.Domain.PriceTags.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.Configurations;

class PriceTagConfiguration : IEntityTypeConfiguration<PriceTag>
{
    public void Configure(EntityTypeBuilder<PriceTag> builder)
    {
        builder.ToTable("PriceTags");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();
    }
}
