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

    public async Task<ICollection<Template>> GetByTypeCodesAsync(
        TemplateTypeCode[] codes, CancellationToken cancellationToken = default)
    {
        return await GetQuery()
            .Where(x => codes.Contains(x.TypeCode))
            .ToListAsync(cancellationToken);
    }

    protected override IQueryable<Template> GetQuery()
    {
        return base.GetQuery()
            .Include(x => x.Type);
    }
}
