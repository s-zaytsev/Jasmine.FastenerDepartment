using Jasmine.FastenerDepartment.Domain.Common.Repositories;
using Jasmine.FastenerDepartment.Domain.Suppliers.Models;

namespace Jasmine.FastenerDepartment.Domain.Suppliers.Repositories;

/// <summary>
/// Suppliers repository.
/// </summary>
public interface ISuppliersRepository : IRepository<Guid, Supplier>
{
    /// <summary>
    /// Returns a collection of extended suppliers.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of extended suppliers.</returns>
    Task<IEnumerable<ExtendedSupplier>> GetAllExtendedSuppliersAsync(CancellationToken cancellationToken);
}
