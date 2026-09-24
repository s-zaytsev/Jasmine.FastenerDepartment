using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content;

namespace Jasmine.FastenerDepartment.Domain.Templates.Providers;

/// <summary>
/// Template prodiver.
/// </summary>
public interface ITemplateProvider
{
    /// <summary>
    /// Renders a template.
    /// </summary>
    /// <param name="code">Template type code.</param>
    /// <param name="data">Template render data.</param>
    /// <returns>Template render result.</returns>
    TemplateRenderResult Render(TemplateTypeCode code, TemplateRenderData data);
}
