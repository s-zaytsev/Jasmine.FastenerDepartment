using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.DataConfigurations;

class ProductChangesReasonDataConfiguration : IEntityTypeConfiguration<ProductChangeReason>
{
    public void Configure(EntityTypeBuilder<ProductChangeReason> builder)
    {
        builder.HasData(
            Create(
                ProductChangeReasonCode.Created,
                new("Created", "Создан")),
            Create(
                ProductChangeReasonCode.ChangedNumber,
                new("Changing the number", "Изменение артикула")),
            Create(
                ProductChangeReasonCode.ChangedName,
                new("Changing the name", "Изменение названия")),
            Create(
                ProductChangeReasonCode.ChangedPrice,
                new("Changing the price", "Изменение цены")),
            Create(
                ProductChangeReasonCode.ChangedPriceTagCode,
                new("Changing the price tag size", "Изменение размера ценника")),
            Create(
                ProductChangeReasonCode.ChangedMeasurementUnitCode,
                new("Changing the measurement unit", "Изменение единицы измерения")),
            Create(
                ProductChangeReasonCode.ChangedOrderStatus,
                new("Changing the order status", "Изменение статуса заказа")),
            Create(
                ProductChangeReasonCode.ChangedPrintStatus,
                new("Changing the print status", "Изменение статуса печати")),
            Create(
                ProductChangeReasonCode.Deleted,
                new("Deleted", "Удален")),
            Create(
                ProductChangeReasonCode.Recovered,
                new("Recovered", "Восстановлен")),
            Create(
                ProductChangeReasonCode.ChangedType,
                new("Changing the type", "Изменение типа")));
    }

    private ProductChangeReason Create(ProductChangeReasonCode id, LocalizedString description)
    {
        return new ProductChangeReason(id, description);
    }
}
