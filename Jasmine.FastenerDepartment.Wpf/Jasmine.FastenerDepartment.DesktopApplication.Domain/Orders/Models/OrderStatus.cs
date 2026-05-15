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
    public string Name { get; set; }
}
