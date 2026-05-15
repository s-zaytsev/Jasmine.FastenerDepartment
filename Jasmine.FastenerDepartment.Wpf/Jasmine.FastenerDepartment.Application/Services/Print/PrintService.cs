using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.Domain.Products.Repositories;
using Jasmine.FastenerDepartment.EF.Repositories.UnitOfWork;

namespace Jasmine.FastenerDepartment.Application.Services.Print;

internal class PrintService : IPrintService
{
    private readonly IProductsRepository _productsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PrintService(
        IProductsRepository productsRepository,
        IUnitOfWork unitOfWork)
    {
        _productsRepository = productsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Product>> GetProductsToPrintAsync(CancellationToken cancellationToken = default)
    {
        var products = await _productsRepository.GetProductsToPrintAsync(cancellationToken);
        return products;
    }

    public async Task RemoveProductFromPrintListAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _productsRepository.GetByIdAsync(id);

        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        product.ChangePrintStatus(false);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAllProductsFromPrintListAsync(CancellationToken cancellationToken = default)
    {
        var products = await _productsRepository.GetProductsToPrintAsync(cancellationToken);

        foreach (var product in products)
        {
            product.ChangePrintStatus(false);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
