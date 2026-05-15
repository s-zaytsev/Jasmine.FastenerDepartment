using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Repositories;
using Jasmine.FastenerDepartment.Domain.Suppliers.Services;
using Jasmine.FastenerDepartment.EF.Repositories.UnitOfWork;

namespace Jasmine.FastenerDepartment.Application.Services.Suppliers;

internal class SupplierProductsService : ISupplierProductsService
{
    private readonly ISupplierProductsRepository _supplierProductsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SupplierProductsService(
        ISupplierProductsRepository supplierProductsRepository,
        IUnitOfWork unitOfWork)
    {
        _supplierProductsRepository = supplierProductsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Page<SupplierProduct>> GetPageAsync(
        SupplierProductsQuery query, CancellationToken cancellationToken)
    {
        var page = await _supplierProductsRepository
            .GetPageAsync<SupplierProductsQuery, SupplierProductsQueryParameter>(query, cancellationToken);

        return page;
    }

    public async Task ChangeAsync(
        Guid id, ChangeSupplierProductModel model, CancellationToken cancellationToken = default)
    {
        var supplierProduct = await _supplierProductsRepository.GetByIdAsync(id, cancellationToken);

        if (supplierProduct == null)
        {
            throw new NotFoundException();
        }

        supplierProduct.ChangeNumber(model.Number);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
