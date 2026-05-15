using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.DataConfigurations;

class OrderStatusDataConfiguration : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.HasData(
            Create(OrderStatusCode.Created, "Created"),
            Create(OrderStatusCode.Sent, "Sent"),
            Create(OrderStatusCode.Fulfilled, "Fulfilled"),
            Create(OrderStatusCode.Cancelled, "Cancelled"));
    }

    private OrderStatus Create(OrderStatusCode id, string name)
    {
        return new OrderStatus
        {
            Id = id,
            Name = name
        };
    }
}
