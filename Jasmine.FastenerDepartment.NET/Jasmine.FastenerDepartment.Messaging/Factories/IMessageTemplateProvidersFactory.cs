using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Messaging.Providers;

namespace Jasmine.FastenerDepartment.Messaging.Factories;

internal interface IMessageTemplateProvidersFactory
{
    IMessageTemplateProvider GetProvider(MessageTypeCode messageType);
}
