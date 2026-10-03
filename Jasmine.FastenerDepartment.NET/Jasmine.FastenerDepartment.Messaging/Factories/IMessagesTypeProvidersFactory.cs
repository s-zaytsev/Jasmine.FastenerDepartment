using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Messaging.MessageProviders;

namespace Jasmine.FastenerDepartment.Messaging.Factories;

internal interface IMessagesTypeProvidersFactory
{
    IMessageTypeProvider GetProvider(MessageTypeCode type);
}
