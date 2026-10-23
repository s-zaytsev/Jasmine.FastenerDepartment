using Jasmine.FastenerDepartment.Domain.Messaging.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.Templates.Emails.Factories;

/// <summary>
/// Email templates factory.
/// </summary>
public interface IEmailTemplatesFactory
{
    /// <summary>
    /// Renders a template.
    /// </summary>
    /// <param name="code">Template type code.</param>
    /// <param name="data">Message request data.</param>
    /// <returns>Template.</returns>
    string Render(TemplateTypeCode code, MessageRequestData data);
}
