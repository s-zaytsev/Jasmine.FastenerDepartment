using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.HistoryEntries.Models;
using Jasmine.FastenerDepartment.Domain.HistoryEntries.Repositories;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.Domain.Products.Repositories;
using Jasmine.FastenerDepartment.Domain.Products.Services;
using Jasmine.FastenerDepartment.Domain.Suppliers.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Repositories;
using Jasmine.FastenerDepartment.EF.Repositories.UnitOfWork;

namespace Jasmine.FastenerDepartment.Application.Services.Products;

/// <summary>
/// Products service.
/// </summary>
public class ProductsService : IProductsService
{
    private readonly IProductsRepository _productsRepository;
    private readonly IProductHistoryRepository _productHistoryRepository;
    private readonly ISupplierProductsRepository _supplierProductsRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Creates service.
    /// </summary>
    /// <param name="productsRepository">Products repository.</param>
    /// <param name="productHistoryRepository">Product history repository.</param>
    /// <param name="supplierProductsRepository">Supplier products repository.</param>
    /// <param name="unitOfWork">Unit of work.</param>
    public ProductsService(
        IProductsRepository productsRepository,
        IProductHistoryRepository productHistoryRepository,
        ISupplierProductsRepository supplierProductsRepository,
        IUnitOfWork unitOfWork)
    {
        _productsRepository = productsRepository;
        _productHistoryRepository = productHistoryRepository;
        _supplierProductsRepository = supplierProductsRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Returns the products page.
    /// </summary>
    /// <param name="query">Query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product page.</returns>
    public async Task<Page<Product>> GetPageAsync(ProductsQuery query, CancellationToken cancellationToken = default)
    {
        var page =
            await _productsRepository.GetPageAsync<ProductsQuery, ProductsQueryParameter>(query, cancellationToken);

        return page;
    }

    /// <summary>
    /// Returns the product.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product.</returns>
    public async Task<Product> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await GetProductByIdAsync(id, cancellationToken);
        return product;
    }

    /// <summary>
    /// Returns the last product number.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The last product number.</returns>
    public async Task<int> GetLastProductNumberAsync(CancellationToken cancellationToken = default)
    {
        var number = await _productsRepository.GetLastProductNumberAsync(cancellationToken);
        return number;
    }

    /// <summary>
    /// Returns the daily history.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Daily history.</returns>
    public async Task<IEnumerable<DailyHistory>> GetDailyHistoryAsync(CancellationToken cancellationToken = default)
    {
        var history = await _productHistoryRepository.GetDailyHistoryAsync(cancellationToken);
        return history;
    }

    /// <summary>
    /// Adds product.
    /// </summary>
    /// <param name="model">Change product model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product.</returns>
    public async Task<Product> AddAsync(ChangeProductModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);

        var lastNumber = await _productsRepository.GetLastProductNumberAsync(cancellationToken);

        var product = new Product(
            ++lastNumber,
            model.Name,
            model.Price,
            model.PriceTagCode,
            model.MeasurementUnitCode,
            model.TypeId,
            model.IsHardwareSizeEnabled,
            model.IsNeededToOrder,
            model.IsNeededToPrint);

        _productsRepository.Add(product);

        if (model.SupplierIds.Any())
        {
            var supplierProducts = model.SupplierIds
                .Select(x => new SupplierProduct(x, product.Id));

            _supplierProductsRepository.AddRange(supplierProducts);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return product;
    }

    /// <summary>
    /// Updates product.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="model">Change product model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task UpdateAsync(Guid id, ChangeProductModel model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);

        var product = await GetProductByIdAsync(id, cancellationToken);

        product.ChangeName(model.Name);
        product.ChangePrice(model.Price);
        product.ChangePriceTagCode(model.PriceTagCode);
        product.ChangeType(model.TypeId);
        product.ChangeMeasurementUnitCode(model.MeasurementUnitCode);
        product.ChangePrintStatus(model.IsNeededToPrint);
        product.ChangeOrderStatus(model.IsNeededToOrder);
        product.ChangeHardwareSizeStatus(model.IsHardwareSizeEnabled);

        await ActualizeProductSuppliersAsync(id, model.SupplierIds, cancellationToken);

        _productsRepository.Update(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Changes the product print status.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task ChangePrintStatusAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        var product = await GetProductByIdAsync(productId, cancellationToken);

        product.ChangePrintStatus(!product.IsNeededToPrint);

        _productsRepository.Update(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Changes the product order status.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task ChangeOrderStatusAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        var product = await GetProductByIdAsync(productId, cancellationToken);

        product.ChangeOrderStatus(!product.IsNeededToOrder);

        _productsRepository.Update(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Deletes the product.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="cancellationToken">Cancellation product.</param>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await GetProductByIdAsync(id, cancellationToken);

        if (product == null || product.IsDeleted)
        {
            return;
        }

        product.ChangeDeletedStatus(true);

        _productsRepository.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Returns product page filters.
    /// </summary>
    /// <param name="query">Products query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product page filters.</returns>
    public async Task<ProductFilters> GetPageFiltersAsync(ProductsQuery query, CancellationToken cancellationToken)
    {
        var pageFilters = await _productsRepository.GetPageFiltersAsync(query, cancellationToken);
        return pageFilters;
    }

    private async Task<Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _productsRepository.GetByIdAsync(id, cancellationToken);
        if (product == null)
        {
            throw new InvalidOperationException($"Product {id} doesn't exist.");
        }

        return product;
    }

    private async Task ActualizeProductSuppliersAsync(
        Guid productId, ICollection<Guid> supplierIds, CancellationToken cancellationToken = default)
    {
        if (!supplierIds.Any())
        {
            _supplierProductsRepository.RemoveProductFromSuppliers(productId);
            return;
        }

        var supplierProducts = await _supplierProductsRepository.GetByProductIdAsync(productId, cancellationToken);

        var supplierProductsToAdd = supplierIds
            .Where(x => !supplierProducts.Any(y => y.SupplierId == x))
            .Select(x => new SupplierProduct(x, productId));

        var supplierProductsToRemove = supplierProducts.Where(x => !supplierIds.Any(y => y == x.SupplierId));

        if (supplierProductsToAdd.Any())
        {
            _supplierProductsRepository.AddRange(supplierProductsToAdd);
        }

        if (supplierProductsToRemove.Any())
        {
            _supplierProductsRepository.RemoveRange(supplierProductsToRemove);
        }
    }
}
