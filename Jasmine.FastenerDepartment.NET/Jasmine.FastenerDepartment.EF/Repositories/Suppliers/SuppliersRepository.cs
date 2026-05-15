using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Repositories;
using Jasmine.FastenerDepartment.EF.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Jasmine.FastenerDepartment.EF.Repositories.Suppliers;

/// <summary>
/// Suppliers repository.
/// </summary>
internal class SuppliersRepository : RepositoryBase<Guid, Supplier>, ISuppliersRepository
{
    /// <summary>
    /// Creates repository.
    /// </summary>
    /// <param name="context">Context.</param>
    public SuppliersRepository(ApplicationDbContext context)
        : base(context)
    { }

    public async Task<IEnumerable<ExtendedSupplier>> GetAllExtendedSuppliersAsync(CancellationToken cancellationToken)
    {
        return await GetQuery()
            .Include(x => x.Products)
            .Select(x => new ExtendedSupplier
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                ProductCount = x.Products.Count,
                ActiveOrderCount = Context
                    .Orders
                    .Count(c =>
                        c.SupplierId.HasValue && c.SupplierId == x.Id &&
                        (c.StatusCode == OrderStatusCode.Created || c.StatusCode == OrderStatusCode.Sent))
            })
            .AsNoTracking()
            .ToListAsync();
    }
}
