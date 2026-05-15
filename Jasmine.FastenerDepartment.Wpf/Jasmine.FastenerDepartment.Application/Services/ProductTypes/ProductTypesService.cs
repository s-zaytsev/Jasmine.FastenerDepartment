using Jasmine.FastenerDepartment.Domain.ProductTypes.Models;
using Jasmine.FastenerDepartment.Domain.ProductTypes.Repositories;
using Jasmine.FastenerDepartment.Domain.ProductTypes.Services;
using Jasmine.FastenerDepartment.EF.Repositories.UnitOfWork;

namespace Jasmine.FastenerDepartment.Application.Services.ProductTypes;

internal class ProductTypesService : IProductTypesService
{
    private readonly IProductTypesRepository _productTypesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductTypesService(
        IProductTypesRepository productTypesRepository,
        IUnitOfWork unitOfWork)
    {
        _productTypesRepository = productTypesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProductType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var types = await _productTypesRepository.GetAllAsync(cancellationToken);
        return types;
    }

    public async Task<IEnumerable<ExtendedProductType>> GetAllExtendedProductTypesAsync(
        CancellationToken cancellationToken = default)
    {
        var extendedTypes = await _productTypesRepository.GetAllExtendedProductTypesAsync(cancellationToken);
        return extendedTypes;
    }

    public async Task<ProductType> CreateAsync(ChangeProductType model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);

        var type = new ProductType(model.Name);

        _productTypesRepository.Add(type);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return type;
    }

    public async Task UpdateAsync(Guid id, ChangeProductType model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);

        var type = await _productTypesRepository.GetByIdAsync(id, cancellationToken);

        if (type == null)
        {
            throw new InvalidOperationException("Product type not found.");
        }

        type.ChangeName(model.Name);

        _productTypesRepository.Update(type);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
