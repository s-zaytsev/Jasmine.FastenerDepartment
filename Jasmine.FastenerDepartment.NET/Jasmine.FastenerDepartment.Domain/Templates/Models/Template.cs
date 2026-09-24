using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content;

namespace Jasmine.FastenerDepartment.Domain.Templates.Models;

/// <summary>
/// Template.
/// </summary>
public class Template : AggregateRootBase<Guid>
{
    /// <summary>
    /// Name.
    /// </summary>
    public Name Name { get; private set; }

    /// <summary>
    /// Type code.
    /// </summary>
    public TemplateTypeCode TypeCode { get; private set; }

    /// <summary>
    /// Type.
    /// </summary>
    public TemplateType Type { get; private set; }

    /// <summary>
    /// Content.
    /// </summary>
    public TemplateContent Content { get; private set; }

    private Template() { }

    /// <summary>
    /// Creates template.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="typeCode">Type code.</param>
    /// <param name="content">Content.</param>
    public Template(
        string name,
        TemplateTypeCode typeCode,
        TemplateContent content)
    {
        Name = new(name);
        TypeCode = typeCode;
        Content = content;
    }

    /// <summary>
    /// Changes a template name.
    /// </summary>
    /// <param name="name">Name.</param>
    public void ChangeName(string name)
    {
        Name = new(name);
    }

    /// <summary>
    /// Changes a type.
    /// </summary>
    /// <param name="code">Template type code.</param>
    public void ChangeType(TemplateTypeCode code)
    {
        TypeCode = code;
    }

    /// <summary>
    /// Changes a template content.
    /// </summary>
    /// <param name="content">Content.</param>
    public void ChangeContent(TemplateContent content)
    {
        Content = content;
    }
}
