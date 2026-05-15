using Jasmine.FastenerDepartment.Domain.ProductsToOrder.Models;
using Jasmine.FastenerDepartment.Domain.ProductsToOrder.Repositories;
using Jasmine.FastenerDepartment.Domain.ProductsToOrder.Services;

namespace Jasmine.FastenerDepartment.Application.Services.ProductsToOrder;

internal class ProductsToOrderService : IProductsToOrderService
{
    private readonly IProductsToOrderRepository _repository;

    public ProductsToOrderService(IProductsToOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductToOrder>> GetAllAsync(
        ProductsToOrderQuery query, CancellationToken cancellationToken = default)
    {
        var products = await _repository.GetAllAsync(query, cancellationToken);
        return products;
    }
}
