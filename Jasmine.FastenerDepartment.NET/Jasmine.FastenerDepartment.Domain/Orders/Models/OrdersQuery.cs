using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Orders.Models;

/// <summary>
/// Orders query.
/// </summary>
public class OrdersQuery : QueryBase<OrdersQueryParameter>
{
    /// <summary>
    /// Only completed orders. 
    /// </summary>
    public bool OnlyCompleted { get; set; }

    /// <summary>
    /// Only active orders.
    /// </summary>
    public bool OnlyActive { get; set; }
}
