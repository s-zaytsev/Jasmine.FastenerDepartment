using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.OrderForm;

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
    /// Renders an order form document.
    /// </summary>
    /// <param name="id">Order identifier.</param>
    /// <param name="model">Render order form model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Template render result.</returns>
    Task<TemplateRenderResult> RenderOrderFormAsync(
        Guid id, RenderOrderForm model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a file model as a zip file.
    /// </summary>
    /// <param name="id">Order identifier.</param>
    /// <param name="model">Download order model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Zip file.</returns>
    Task<FileModel> DownloadOrderFilesAsZipAsync(
        Guid id, DownloadOrderModel model, CancellationToken cancellationToken = default);
}
