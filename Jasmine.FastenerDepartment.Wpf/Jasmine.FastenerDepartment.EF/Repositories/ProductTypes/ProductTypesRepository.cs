using Jasmine.FastenerDepartment.Domain.ProductTypes.Models;
using Jasmine.FastenerDepartment.Domain.ProductTypes.Repositories;
using Jasmine.FastenerDepartment.EF.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Jasmine.FastenerDepartment.EF.Repositories.ProductTypes;

internal class ProductTypesRepository : RepositoryBase<Guid, ProductType>, IProductTypesRepository
{
    public ProductTypesRepository(ApplicationDbContext context)
        : base(context)
    { }

    public async Task<IEnumerable<ExtendedProductType>> GetAllExtendedProductTypesAsync(
        CancellationToken cancellationToken = default)
    {
        return await GetQuery()
            .Select(x => new ExtendedProductType
            {
                Id = x.Id,
                Name = x.Name,
                ProductCount = Context.Products.Count(c => c.TypeId == x.Id)
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
