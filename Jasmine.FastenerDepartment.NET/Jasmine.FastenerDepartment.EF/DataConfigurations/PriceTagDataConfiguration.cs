using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;
using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.EF.DataConfigurations;

class PriceTagDataConfiguration : IEntityTypeConfiguration<PriceTag>
{
    public void Configure(EntityTypeBuilder<PriceTag> builder)
    {
        builder.HasData(
            Create(PriceTagCode.S, new("Size S", "Размер S")),
            Create(PriceTagCode.L, new("Size L", "Размер L")),
            Create(PriceTagCode.M, new("Size M", "Размер M")),
            Create(PriceTagCode.XL, new("Size XL", "Размер XL")));
    }

    private PriceTag Create(PriceTagCode id, LocalizedString name)
    {
        return new PriceTag(id, name);
    }
}