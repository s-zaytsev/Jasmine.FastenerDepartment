using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content;

namespace Jasmine.FastenerDepartment.Domain.Templates.Repositories;

internal class TemplateContentTableColumnsRepository : ITemplateContentTableColumnsRepository
{
    public IReadOnlyDictionary<TemplateTypeCode, IReadOnlyDictionary<Enum, LocalizedString>> GetAll()
    {
        var columns = ContentTableColumns.Columns;
        return columns;
    }

    public IReadOnlyDictionary<Enum, LocalizedString> GetByTemplateTypeCode(TemplateTypeCode code)
    {
        if (ContentTableColumns.Columns.TryGetValue(code, out var value))
            return value;
        else
            return new Dictionary<Enum, LocalizedString>();
    }
}
