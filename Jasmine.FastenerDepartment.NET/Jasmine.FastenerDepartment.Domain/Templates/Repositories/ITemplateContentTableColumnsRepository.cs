using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.Domain.Templates.Repositories;

/// <summary>
/// Template content table columns repository.
/// </summary>
public interface ITemplateContentTableColumnsRepository
{
    /// <summary>
    /// Returns a collection of content table columns.
    /// </summary>
    /// <returns>Collection of content table columns.</returns>
    IReadOnlyDictionary<TemplateTypeCode, IReadOnlyDictionary<Enum, LocalizedString>> GetAll();

    /// <summary>
    /// Returns a collection of content table columns by template type code.
    /// </summary>
    /// <returns>Collection of content table columns.</returns>
    IReadOnlyDictionary<Enum, LocalizedString> GetByTemplateTypeCode(TemplateTypeCode code);
}
