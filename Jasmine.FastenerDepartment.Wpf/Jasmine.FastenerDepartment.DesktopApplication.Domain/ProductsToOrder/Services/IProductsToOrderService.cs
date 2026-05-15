using Jasmine.FastenerDepartment.Domain.ProductsToOrder.Models;

namespace Jasmine.FastenerDepartment.Domain.ProductsToOrder.Services;

public interface IProductsToOrderService
{
    /// <summary>
    /// Returns the list of products to order.
    /// </summary>
    /// <param name="query">Products to order query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of products to order.</returns>
    Task<IEnumerable<ProductToOrder>> GetAllAsync(
        ProductsToOrderQuery query, CancellationToken cancellationToken = default);
}
