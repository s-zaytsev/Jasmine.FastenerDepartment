using Jasmine.FastenerDepartment.Domain.ProductsToOrder.Models;
using Jasmine.FastenerDepartment.Domain.ProductsToOrder.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Jasmine.FastenerDepartment.EF.Repositories.ProductsToOrder;

internal class ProductsToOrderRepository : IProductsToOrderRepository
{
    private readonly ApplicationDbContext _context;

    public ProductsToOrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductToOrder>> GetAllAsync(
        ProductsToOrderQuery query, CancellationToken cancellationToken = default)
    {
        var queryDb = _context.Products.Where(x => !x.IsDeleted).AsQueryable();

        queryDb = queryDb.Where(x => x.IsNeededToOrder);

        if (query.SupplierId.HasValue)
        {
            queryDb = queryDb.Where(x => x.Suppliers.Any(a => a.Id == query.SupplierId.Value));
        }

        if (query.OnlyToOrder)
        {
            queryDb = queryDb.Where(x => x.IsNeededToOrder);
        }

        queryDb = queryDb.Include(x => x.Suppliers)
            .ThenInclude(x => x.Products)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.Type);

        var products = await queryDb
            .Select(x => new ProductToOrder
            {
                Product = x,
                SupplierNumbers = x.Suppliers
                    .Select(s => s.Products.Where(w => w.ProductId == x.Id)
                    .Select(s => new SupplierNumber { SupplierId = s.SupplierId, Number = s.Number }))
                    .SelectMany(s => s)
                    .ToList()
            })
            .ToListAsync(cancellationToken);

        return products;
    }
}
