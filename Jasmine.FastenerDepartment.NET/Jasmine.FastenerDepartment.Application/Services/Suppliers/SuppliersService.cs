using Jasmine.FastenerDepartment.Domain.Suppliers.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Repositories;
using Jasmine.FastenerDepartment.Domain.Suppliers.Services;
using Jasmine.FastenerDepartment.EF.Repositories.UnitOfWork;

namespace Jasmine.FastenerDepartment.Application.Services.Suppliers;

/// <summary>
/// Suppliers service.
/// </summary>
public class SuppliersService : ISuppliersService
{
    private readonly ISuppliersRepository _suppliersRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Creates the service.
    /// </summary>
    /// <param name="suppliersRepository">Suppliers repository.</param>
    /// <param name="unitOfWork">Unit of work.</param>
    public SuppliersService(
        ISuppliersRepository suppliersRepository,
        IUnitOfWork unitOfWork)
    {
        _suppliersRepository = suppliersRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Returns the list of suppliers.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of suppliers.</returns>
    public async Task<IEnumerable<Supplier>> GetAllAsync(CancellationToken cancellationToken)
    {
        var suppliers = await _suppliersRepository.GetAllAsync(cancellationToken);
        return suppliers;
    }

    /// <summary>
    /// Returns a collection of extended suppliers.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of extended suppliers.</returns>
    public async Task<IEnumerable<ExtendedSupplier>> GetAllExtendedSuppliersAsync(CancellationToken cancellationToken)
    {
        var extendedSuppliers = await _suppliersRepository.GetAllExtendedSuppliersAsync(cancellationToken);
        return extendedSuppliers;
    }

    /// <summary>
    /// Returns the supplier.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Supplier.</returns>
    public async Task<Supplier> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var supplier = await GetSupplierByIdAsync(id, cancellationToken);
        return supplier;
    }

    /// <summary>
    /// Adds the supplier.
    /// </summary>
    /// <param name="model">Change supplier model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Supplier.</returns>
    public async Task<Supplier> AddAsync(ChangeSupplierModel model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(model);

        var supplier = ApplicationMapper.Map(model);

        _suppliersRepository.Add(supplier);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return supplier;
    }

    /// <summary>
    /// Updates the supplier.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="model">Change supplier model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task UpdateAsync(Guid id, ChangeSupplierModel model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(model);

        var supplier = await GetSupplierByIdAsync(id, cancellationToken);

        supplier.ChangeName(model.Name);
        supplier.ChangeAddress(model.Address);

        _suppliersRepository.Update(supplier);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Removes the supplier.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task RemoveSupplierAsync(Guid id, CancellationToken cancellationToken)
    {
        var supplier = await GetSupplierByIdAsync(id, cancellationToken);

        _suppliersRepository.Remove(supplier);

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task<Supplier> GetSupplierByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var supplier = await _suppliersRepository.GetByIdAsync(id, cancellationToken);

        if (supplier == null)
        {
            throw new InvalidOperationException($"Supplier {id} isn't found.");
        }

        return supplier;
    }
}
