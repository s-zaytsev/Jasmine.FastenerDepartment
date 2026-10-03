using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.Domain.Templates;

/// <summary>
/// Templates query.
/// </summary>
public class TemplatesQuery
{
    /// <summary>
    /// Template codes.
    /// </summary>
    public ICollection<TemplateTypeCode> TypeCodes { get; set; }
}
