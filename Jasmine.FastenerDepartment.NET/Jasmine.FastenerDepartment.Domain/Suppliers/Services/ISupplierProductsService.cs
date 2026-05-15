using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Models;

namespace Jasmine.FastenerDepartment.Domain.Suppliers.Services;

/// <summary>
/// Supplier products service.
/// </summary>
public interface ISupplierProductsService
{
    /// <summary>
    /// Returns supplier products page.
    /// </summary>
    /// <param name="query">Supplier products query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Supplier products page.</returns>
    Task<Page<SupplierProduct>> GetPageAsync(
        SupplierProductsQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Changes the supplier product.
    /// </summary>
    /// <param name="id">Supplier product identifier.</param>
    /// <param name="model">Change supplier product model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task ChangeAsync(
        Guid id, ChangeSupplierProductModel model, CancellationToken cancellationToken = default);
}
