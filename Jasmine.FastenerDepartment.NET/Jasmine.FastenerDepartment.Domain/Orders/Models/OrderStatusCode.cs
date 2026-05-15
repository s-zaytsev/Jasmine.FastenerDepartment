namespace Jasmine.FastenerDepartment.Domain.Orders.Models;

/// <summary>
/// Order status code.
/// </summary>
public enum OrderStatusCode
{
    /// <summary>
    /// Created.
    /// </summary>
    Created = 1,

    /// <summary>
    /// Sent.
    /// </summary>
    Sent = 2,

    /// <summary>
    /// Fulfilled.
    /// </summary>
    Fulfilled = 3,

    /// <summary>
    /// Cancelled.
    /// </summary>
    Cancelled = 4
}
