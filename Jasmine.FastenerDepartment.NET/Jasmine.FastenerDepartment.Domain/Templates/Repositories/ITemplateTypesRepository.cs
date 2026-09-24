using Jasmine.FastenerDepartment.Domain.Common.Repositories;
using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.Domain.Templates.Repositories;

/// <summary>
/// Template types repository.
/// </summary>
public interface ITemplateTypesRepository : IEntitiesRepository<TemplateTypeCode,  TemplateType>
{ }
