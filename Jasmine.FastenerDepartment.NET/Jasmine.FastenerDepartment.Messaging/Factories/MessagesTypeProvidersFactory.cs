using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Messaging.MessageProviders;
using Microsoft.Extensions.DependencyInjection;

namespace Jasmine.FastenerDepartment.Messaging.Factories;

internal class MessagesTypeProvidersFactory : IMessagesTypeProvidersFactory
{
    private readonly IServiceProvider _serviceProvider;

    public MessagesTypeProvidersFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IMessageTypeProvider GetProvider(MessageTypeCode type)
    {
        return type switch
        {
            MessageTypeCode.Email => _serviceProvider.GetRequiredService<EmailsProvider>(),
            _ => throw new NotSupportedException($"Message type {type} not supported."),
        };
    }
}
