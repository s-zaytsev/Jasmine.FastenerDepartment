using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;

namespace Jasmine.FastenerDepartment.EF.DataConfigurations;

class PriceTagDataConfiguration : IEntityTypeConfiguration<PriceTag>
{
    public void Configure(EntityTypeBuilder<PriceTag> builder)
    {
        builder.HasData(
            Create(PriceTagCode.S, "S"),
            Create(PriceTagCode.L, "L"),
            Create(PriceTagCode.M, "M"),
            Create(PriceTagCode.XL, "XL"));
    }

    private PriceTag Create(PriceTagCode id, string name)
    {
        return new PriceTag(id, name);
    }
}