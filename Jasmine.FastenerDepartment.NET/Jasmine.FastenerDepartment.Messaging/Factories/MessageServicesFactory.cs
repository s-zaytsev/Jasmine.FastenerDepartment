using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Messaging.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Jasmine.FastenerDepartment.Messaging.Factories;

internal class MessageServicesFactory : IMessageServicesFactory
{
    private readonly IServiceProvider _serviceProvider;

    public MessageServicesFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IMessageService GetService(MessageType type)
    {
        return type switch
        {
            MessageType.Email => _serviceProvider.GetRequiredService<IEmailService>(),
            _ => throw new NotSupportedException($"Message type {type} not supported."),
        };
    }
}
