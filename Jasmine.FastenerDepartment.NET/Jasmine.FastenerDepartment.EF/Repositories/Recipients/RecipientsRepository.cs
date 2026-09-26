using Jasmine.FastenerDepartment.Domain.Recipients.Models;
using Jasmine.FastenerDepartment.Domain.Recipients.Repositories;
using Jasmine.FastenerDepartment.EF.Repositories.Common;

namespace Jasmine.FastenerDepartment.EF.Repositories.Recipients;

internal class RecipientsRepository : RepositoryBase<Guid, Recipient>, IRecipientsRepository
{
    public RecipientsRepository(ApplicationDbContext context)
        : base(context)
    {}
}
