using Jasmine.FastenerDepartment.Domain.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.DataConfigurations;

class ProductChangesReasonDataConfiguration : IEntityTypeConfiguration<ProductChangeReason>
{
    public void Configure(EntityTypeBuilder<ProductChangeReason> builder)
    {
        builder.HasData(
            Create(ProductChangeReasonCode.Created, "Product was created"),
            Create(ProductChangeReasonCode.ChangedNumber, "Product number was changed"),
            Create(ProductChangeReasonCode.ChangedName, "Product name was changed"),
            Create(ProductChangeReasonCode.ChangedPrice, "Product price was changed"),
            Create(ProductChangeReasonCode.ChangedPriceTagCode, "Product price tag code was changed"),
            Create(ProductChangeReasonCode.ChangedMeasurementUnitCode, "Product measurement unit code was changed"),
            Create(ProductChangeReasonCode.ChangedOrderStatus, "Product order status was changed"),
            Create(ProductChangeReasonCode.ChangedPrintStatus, "Product print status was changed"),
            Create(ProductChangeReasonCode.Deleted, "Product was deleted"),
            Create(ProductChangeReasonCode.Recovered, "Product was recovered"),
            Create(ProductChangeReasonCode.ChangedType, "Product type was changed"));
    }

    private ProductChangeReason Create(ProductChangeReasonCode id, string description)
    {
        return new ProductChangeReason(id, description);
    }
}
