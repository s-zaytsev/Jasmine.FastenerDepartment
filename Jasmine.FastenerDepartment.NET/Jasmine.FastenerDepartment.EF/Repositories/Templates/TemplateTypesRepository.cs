using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Repositories;
using Jasmine.FastenerDepartment.EF.Repositories.Common;

namespace Jasmine.FastenerDepartment.EF.Repositories.Templates;

internal class TemplateTypesRepository :
    EntitiesRepositoryBase<TemplateTypeCode, TemplateType>, ITemplateTypesRepository
{
    public TemplateTypesRepository(ApplicationDbContext context)
        : base(context)
    { }
}
