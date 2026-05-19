using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Orders.Models;

/// <summary>
/// Order status.
/// </summary>
public class OrderStatus : EntityBase<OrderStatusCode>
{
    /// <summary>
    /// Order status name.
    /// </summary>
    public LocalizedString Name { get; init; }

    private OrderStatus() { }

    /// <summary>
    /// Creates order status.
    /// </summary>
    public OrderStatus(
        OrderStatusCode id,
        LocalizedString name)
    {
        Id = id;
        Name = name;
    }
}
