using Jasmine.FastenerDepartment.Domain.Common.Repositories;
using Jasmine.FastenerDepartment.Domain.Products.Models;

namespace Jasmine.FastenerDepartment.Domain.Products.Repositories;

/// <summary>
/// Products repository.
/// </summary>
public interface IProductsRepository : IRepository<Guid, Product>
{
    /// <summary>
    /// Returns the list of products with history.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of products.</returns>
    Task<IEnumerable<Product>> GetAllWithHistoryAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns product by number.
    /// </summary>
    /// <param name="number">Product number.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product.</returns>
    Task<Product> GetByNumberAsync(int number, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns last product number.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Last product number.</returns>
    Task<int> GetLastProductNumberAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the list of products to print.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of products to print.</returns>
    Task<IEnumerable<Product>> GetProductsToPrintAsync(CancellationToken cancellationToken= default);

    /// <summary>
    /// Returns the list of products by identifiers.
    /// </summary>
    /// <param name="ids">Identifiers.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of products by identifiers.</returns>
    Task<ICollection<Product>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns product page filters.
    /// </summary>
    /// <param name="query">Products query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product page filters.</returns>
    Task<ProductFilters> GetPageFiltersAsync(ProductsQuery query, CancellationToken cancellationToken);
}
