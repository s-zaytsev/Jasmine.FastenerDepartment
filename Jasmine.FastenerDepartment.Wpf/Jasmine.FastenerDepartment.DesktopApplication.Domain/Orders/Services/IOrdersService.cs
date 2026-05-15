using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;

namespace Jasmine.FastenerDepartment.Domain.Orders.Services;

/// <summary>
/// Orders service.
/// </summary>
public interface IOrdersService
{
    /// <summary>
    /// Returns the orders page.
    /// </summary>
    /// <param name="query">Orders query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Orders page.</returns>
    Task<Page<Order>> GetPageAsync(OrdersQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the order.
    /// </summary>
    /// <param name="id">Order identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Order.</returns>
    Task<Order> GetAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates new order.
    /// </summary>
    /// <param name="model">Create order model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Order.</returns>
    Task<Order> CreateAsync(CreateOrderModel model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the order.
    /// </summary>
    /// <param name="id">Order identifier.</param>
    /// <param name="model">Change order model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task UpdateAsync(Guid id, ChangeOrderModel model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends the order.
    /// </summary>
    /// <param name="id">Order identifier.</param>
    /// <param name="model">Send order model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task SendAsync(Guid id, SendOrderModel model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Completes the order.
    /// </summary>
    /// <param name="id">Order identifier.</param>
    /// <param name="model">Complete order model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task CompleteAsync(Guid id, CompleteOrderModel model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels the order.
    /// </summary>
    /// <param name="id">Order identifier.</param>
    /// <param name="model">Cancel order model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task CancelAsync(Guid id, CancelOrderModel model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns an order document stream.
    /// </summary>
    /// <param name="id">Order Identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Order document stream.</returns>
    Task<FileStreamModel> GetOrderDocumentStreamAsync(Guid id, CancellationToken cancellationToken = default);
}
