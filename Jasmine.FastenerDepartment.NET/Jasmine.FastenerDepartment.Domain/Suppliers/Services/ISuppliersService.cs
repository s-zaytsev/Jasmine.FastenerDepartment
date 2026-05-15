using Jasmine.FastenerDepartment.Domain.Suppliers.Models;

namespace Jasmine.FastenerDepartment.Domain.Suppliers.Services;

/// <summary>
/// Suppliers service.
/// </summary>
public interface ISuppliersService
{
    /// <summary>
    /// Returns the list of suppliers.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of suppliers.</returns>
    Task<IEnumerable<Supplier>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a collection of extended suppliers.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of extended suppliers.</returns>
    Task<IEnumerable<ExtendedSupplier>> GetAllExtendedSuppliersAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the supplier.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Supplier.</returns>
    Task<Supplier> GetAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds the supplier.
    /// </summary>
    /// <param name="model">Change supplier model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Supplier.</returns>
    Task<Supplier> AddAsync(ChangeSupplierModel model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the supplier.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="model">Change supplier model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task UpdateAsync(Guid id, ChangeSupplierModel model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes the supplier.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task RemoveSupplierAsync(Guid id, CancellationToken cancellationToken = default);
}
