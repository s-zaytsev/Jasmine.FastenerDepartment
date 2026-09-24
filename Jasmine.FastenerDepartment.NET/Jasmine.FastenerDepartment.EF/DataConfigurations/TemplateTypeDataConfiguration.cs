using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.DataConfigurations;

internal class TemplateTypeDataConfiguration : IEntityTypeConfiguration<TemplateType>
{
    public void Configure(EntityTypeBuilder<TemplateType> builder)
    {
        builder.HasData(
            Create(TemplateTypeCode.OrderForm, new("Order form", "Бланк заказа")),
            Create(TemplateTypeCode.ProductCatalog, new("Product catalog", "Каталог товаров")));
    }

    private TemplateType Create(TemplateTypeCode id, LocalizedString name)
    {
        return new TemplateType(id, name);
    }
}
