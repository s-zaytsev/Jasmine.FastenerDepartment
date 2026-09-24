using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content;

namespace Jasmine.FastenerDepartment.Domain.Templates.Factories;

/// <summary>
/// Template dactory.
/// </summary>
public interface ITemplateFactory
{
    /// <summary>
    /// Render a template.
    /// </summary>
    /// <param name="templateFormat">Template format.</param>
    /// <param name="templateType">Template type.</param>
    /// <param name="data">Template data.</param>
    /// <returns>Template render result.</returns>
    TemplateRenderResult Render(
        TemplateFormatCode templateFormat,
        TemplateTypeCode templateType,
        TemplateRenderData data);
}
