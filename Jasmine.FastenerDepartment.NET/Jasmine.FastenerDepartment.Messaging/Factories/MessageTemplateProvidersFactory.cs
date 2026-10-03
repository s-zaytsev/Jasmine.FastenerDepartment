using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Messaging.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Jasmine.FastenerDepartment.Messaging.Factories;

internal class MessageTemplateProvidersFactory : IMessageTemplateProvidersFactory
{
    private readonly IServiceProvider _serviceProvider;

    public MessageTemplateProvidersFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IMessageTemplateProvider GetProvider(MessageTypeCode messageType)
    {
        return messageType switch
        {
            MessageTypeCode.Email => _serviceProvider.GetRequiredService<EmailsTemplateProvider>(),
            _ => throw new NotSupportedException($"Message type {messageType} not supported.")
        };
    }
}
