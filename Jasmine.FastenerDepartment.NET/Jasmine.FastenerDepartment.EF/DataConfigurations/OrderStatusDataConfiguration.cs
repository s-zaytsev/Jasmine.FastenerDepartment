using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.DataConfigurations;

class OrderStatusDataConfiguration : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.HasData(
            Create(OrderStatusCode.Created, new("Created", "Создан")),
            Create(OrderStatusCode.Sent, new("Sent", "Отправлен")),
            Create(OrderStatusCode.Fulfilled, new("Fulfilled", "Доставлен")),
            Create(OrderStatusCode.Cancelled, new("Cancelled", "Отменен")));
    }

    private OrderStatus Create(OrderStatusCode id, LocalizedString name)
    {
        return new OrderStatus(id, name);
    }
}
