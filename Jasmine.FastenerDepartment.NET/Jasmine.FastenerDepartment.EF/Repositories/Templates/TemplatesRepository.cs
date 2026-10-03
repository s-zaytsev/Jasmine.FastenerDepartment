using Jasmine.FastenerDepartment.Domain.Templates;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Repositories;
using Jasmine.FastenerDepartment.EF.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Jasmine.FastenerDepartment.EF.Repositories.Templates;

internal class TemplatesRepository : RepositoryBase<Guid, Template>, ITemplatesRepository
{
    public TemplatesRepository(ApplicationDbContext context)
        : base(context)
    { }

    public async Task<ICollection<Template>> GetAsync(
        TemplatesQuery query, CancellationToken cancellationToken = default)
    {
        var queryDb = GetQuery();

        if (query.TypeCodes?.Count > 0)
        {
            queryDb = queryDb.Where(x => query.TypeCodes.Contains(x.TypeCode));
        }

        return await queryDb.ToListAsync(cancellationToken);
    }

    protected override IQueryable<Template> GetQuery()
    {
        return base.GetQuery()
            .Include(x => x.Type);
    }
}
