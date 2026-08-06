using Jasmine.FastenerDepartment.Domain.Common.Repositories;
using Jasmine.FastenerDepartment.Domain.Companies.Models;

namespace Jasmine.FastenerDepartment.Domain.Companies.Repositories;

/// <summary>
/// Companies repository.
/// </summary>
public interface ICompaniesRepository : IRepository<Guid, Company>
{ }
