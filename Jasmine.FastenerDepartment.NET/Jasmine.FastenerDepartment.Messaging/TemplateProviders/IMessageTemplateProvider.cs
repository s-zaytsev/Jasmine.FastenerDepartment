using Jasmine.FastenerDepartment.Domain.Messaging.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.Messaging.Providers;

internal interface IMessageTemplateProvider
{
    string Render(TemplateTypeCode code, MessageRequestData data);
}
