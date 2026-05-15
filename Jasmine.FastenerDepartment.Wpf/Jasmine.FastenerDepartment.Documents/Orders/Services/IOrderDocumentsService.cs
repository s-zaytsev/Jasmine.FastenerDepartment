using Jasmine.FastenerDepartment.Domain.Orders.Models;

namespace Jasmine.FastenerDepartment.Documents.Orders.Services;

/// <summary>
/// Order documents service.
/// </summary>
public interface IOrderDocumentsService
{
    /// <summary>
    /// Returns a order document stream.
    /// </summary>
    /// <param name="order">Order.</param>
    /// <returns>Order document stream.</returns>
    Task<Stream> GetStreamAsync(Order order);
}
