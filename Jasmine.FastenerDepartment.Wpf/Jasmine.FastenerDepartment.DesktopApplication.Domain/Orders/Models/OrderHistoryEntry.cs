using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Orders.Models;

/// <summary>
/// Order history entry.
/// </summary>
public class OrderHistoryEntry : EntityBase<Guid>
{
    /// <summary>
    /// Order identifier.
    /// </summary>
    public Guid OrderId { get; init; }

    /// <summary>
    /// Created date.
    /// </summary>
    public DateTime CreatedDate { get; private set; }

    /// <summary>
    /// Order status code.
    /// </summary>
    public OrderStatusCode OrderStatusCode { get; private set; }

    /// <summary>
    /// Comment.
    /// </summary>
    public string Comment { get; private set; }

    private OrderHistoryEntry() { }

    /// <summary>
    /// Creates order history entry.
    /// </summary>
    /// <param name="createdDate">Created date.</param>
    /// <param name="orderStatusCode">Order status code.</param>
    /// <param name="comment">Comment.</param>
    public OrderHistoryEntry(DateTime createdDate, OrderStatusCode orderStatusCode, string comment)
    {
        CreatedDate = createdDate;
        OrderStatusCode = orderStatusCode;
        Comment = comment;
    }
}
