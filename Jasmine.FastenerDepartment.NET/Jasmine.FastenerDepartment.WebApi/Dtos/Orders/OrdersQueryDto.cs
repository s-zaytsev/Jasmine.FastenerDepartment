using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Orders.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Orders;

/// <summary>
/// Orders query.
/// </summary>
public class OrdersQueryDto : QueryBase<OrdersQueryParameter>
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
