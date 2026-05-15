using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Repositories;
using Jasmine.FastenerDepartment.EF.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jasmine.FastenerDepartment.EF.Repositories.Suppliers;

internal class SupplierProductsRepository
    : RepositoryBase<Guid, SupplierProduct>, ISupplierProductsRepository
{
    public SupplierProductsRepository(ApplicationDbContext context)
        : base(context)
    { }

    public async Task<IEnumerable<SupplierProduct>> GetByProductIdAsync(
        Guid productId, CancellationToken cancellationToken = default)
    {
        return await GetQuery().Where(x => x.ProductId == productId).ToListAsync(cancellationToken);
    }

    public async Task<SupplierProduct> GetByProductIdAsync(
        Guid productId, Guid supplierId, CancellationToken cancellationToken = default)
    {
        return await GetQuery()
            .FirstOrDefaultAsync(x => x.ProductId == productId && x.SupplierId == supplierId, cancellationToken);
    }

    public void RemoveProductFromSuppliers(Guid productId)
    {
        GetQuery().Where(x => x.ProductId == productId).ExecuteDelete();
    }

    protected override IEnumerable<Expression<Func<SupplierProduct, bool>>> GetFilterings<T, K>(
        IQueryable<SupplierProduct> dbQuery, T query)
    {
        var supplierProductsQuery = query as SupplierProductsQuery;

        var predicates = new List<Expression<Func<SupplierProduct, bool>>>()
        {
            x => !x.Product.IsDeleted,
            x => x.SupplierId == supplierProductsQuery.SupplierId
        };

        if (!string.IsNullOrWhiteSpace(supplierProductsQuery.Search))
        {
            predicates.Add(
                x => x.Product.Name.Value.ToLower().Contains(supplierProductsQuery.Search.ToLower()) ||
                     x.Product.Number.Value.ToString().Contains(supplierProductsQuery.Search));
        }

        return predicates;
    }

    protected override Expression<Func<SupplierProduct, object>> GetSorting<E>(E parameter)
    {
        return parameter switch
        {
            ProductsQueryParameter.ProductNumber => x => x.Product.Number.Value,
            ProductsQueryParameter.Name => x => x.Product.Name.Value,
            ProductsQueryParameter.Price => x => x.Product.Price.Value,
            ProductsQueryParameter.PriceTag => x => x.Product.PriceTagCode,
            _ => x => x.Product.Id,
        };
    }

    protected override IQueryable<SupplierProduct> GetQuery()
    {
        return base.GetQuery()
            .Include(x => x.Product);
    }
}
