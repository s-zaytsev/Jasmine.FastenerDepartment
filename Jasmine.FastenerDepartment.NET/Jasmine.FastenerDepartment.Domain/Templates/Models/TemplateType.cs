using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Templates.Models;

/// <summary>
/// Template type.
/// </summary>
public class TemplateType : EntityBase<TemplateTypeCode>
{
    /// <summary>
    /// Name.
    /// </summary>
    public LocalizedString Name { get; init; }

    private TemplateType() { }

    /// <summary>
    /// Creates a template type.
    /// </summary>
    /// <param name="id">Template type code.</param>
    /// <param name="name">Name.</param>
    public TemplateType(
        TemplateTypeCode id,
        LocalizedString name)
    {
        Id = id;
        Name = name;
    }
}
