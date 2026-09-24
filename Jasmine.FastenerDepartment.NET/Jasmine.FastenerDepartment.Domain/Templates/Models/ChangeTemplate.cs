using Jasmine.FastenerDepartment.Domain.Templates.Models.Content;

namespace Jasmine.FastenerDepartment.Domain.Templates.Models;

/// <summary>
/// Change template model.
/// </summary>
public class ChangeTemplate
{
    /// <summary>
    /// Template name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Template type code.
    /// </summary>
    public TemplateTypeCode TypeCode { get; set; }

    /// <summary>
    /// Template content.
    /// </summary>
    public TemplateContent Content { get; set; }
}
