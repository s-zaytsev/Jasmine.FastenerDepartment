using Jasmine.FastenerDepartment.Domain.Common.Repositories;
using Jasmine.FastenerDepartment.Domain.Recipients.Models;

namespace Jasmine.FastenerDepartment.Domain.Recipients.Repositories;

/// <summary>
/// Recipients repository.
/// </summary>
public interface IRecipientsRepository : IRepository<Guid, Recipient>
{ }
