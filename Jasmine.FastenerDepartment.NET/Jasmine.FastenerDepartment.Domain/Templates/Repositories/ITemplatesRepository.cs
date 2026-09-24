using Jasmine.FastenerDepartment.Domain.Common.Repositories;
using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.Domain.Templates.Repositories;

/// <summary>
/// Templates repository.
/// </summary>
public interface ITemplatesRepository : IRepository<Guid, Template>
{ }
