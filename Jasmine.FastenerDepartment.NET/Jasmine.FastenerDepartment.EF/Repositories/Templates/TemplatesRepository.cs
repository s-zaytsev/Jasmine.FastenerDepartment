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

    protected override IQueryable<Template> GetQuery()
    {
        return base.GetQuery()
            .Include(x => x.Type);
    }
}
