using Jasmine.FastenerDepartment.Domain.Common.Repositories;
using Jasmine.FastenerDepartment.Domain.Suppliers.Models;

namespace Jasmine.FastenerDepartment.Domain.Suppliers.Repositories;

/// <summary>
/// Supplier products repository.
/// </summary>
public interface ISupplierProductsRepository : IRepository<Guid, SupplierProduct>
{
    /// <summary>
    /// Returns list of suppliers products by product identifier.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of suppliers products.</returns>
    Task<IEnumerable<SupplierProduct>> GetByProductIdAsync(
        Guid productId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the suppliers product by product and supplier identifier.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    /// <param name="supplierId">Supplier identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of suppliers products.</returns>
    Task<SupplierProduct> GetByProductIdAsync(
        Guid productId, Guid supplierId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes product from suppliers by product identifier.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    void RemoveProductFromSuppliers(Guid productId);
}
