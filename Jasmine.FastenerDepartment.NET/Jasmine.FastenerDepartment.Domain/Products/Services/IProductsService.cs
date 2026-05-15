using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.HistoryEntries.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;

namespace Jasmine.FastenerDepartment.Domain.Products.Services;

/// <summary>
/// Products service.
/// </summary>
public interface IProductsService
{
    /// <summary>
    /// Returns the products page.
    /// </summary>
    /// <param name="query">Query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product page.</returns>
    Task<Page<Product>> GetPageAsync(ProductsQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the product.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product.</returns>
    Task<Product> GetAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the last product number.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The last product number.</returns>
    Task<int> GetLastProductNumberAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the daily history.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Daily history.</returns>
    Task<IEnumerable<DailyHistory>> GetDailyHistoryAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds product.
    /// </summary>
    /// <param name="model">Change product model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product.</returns>
    Task<Product> AddAsync(ChangeProductModel model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates product.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="model">Change product model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task UpdateAsync(Guid id, ChangeProductModel model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Changes the product print status.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task ChangePrintStatusAsync(Guid productId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Changes the product order status.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task ChangeOrderStatusAsync(Guid productId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the product.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="cancellationToken">Cancellation product.</param>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns product page filters.
    /// </summary>
    /// <param name="query">Products query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product page filters.</returns>
    Task<ProductFilters> GetPageFiltersAsync(ProductsQuery query, CancellationToken cancellationToken);
}
