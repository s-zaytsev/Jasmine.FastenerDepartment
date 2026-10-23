using Jasmine.FastenerDepartment.Domain.Messaging.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Templates.Emails.Factories;

namespace Jasmine.FastenerDepartment.Messaging.TemplateProviders;

internal class EmailsTemplateProvider : IMessageTemplateProvider
{
    private readonly IEmailTemplatesFactory _emailTemplatesFactory;

    public EmailsTemplateProvider(IEmailTemplatesFactory emailTemplatesFactory)
    {
        _emailTemplatesFactory = emailTemplatesFactory;
    }

    public string Render(TemplateTypeCode code, MessageRequestData data)
    {
        var template = _emailTemplatesFactory.Render(code, data);
        return template;
    }
}
