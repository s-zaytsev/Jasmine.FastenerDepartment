using Jasmine.FastenerDepartment.Domain.Companies.Models;
using Jasmine.FastenerDepartment.Domain.Companies.Repositories;
using Jasmine.FastenerDepartment.EF.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Jasmine.FastenerDepartment.EF.Repositories.Companies;

internal class CompaniesRepository : RepositoryBase<Guid, Company>, ICompaniesRepository
{
    public CompaniesRepository(ApplicationDbContext context)
        : base(context)
    { }

    protected override IQueryable<Company> GetQuery()
    {
        return base.GetQuery()
            .Include(x => x.Type);
    }
}
