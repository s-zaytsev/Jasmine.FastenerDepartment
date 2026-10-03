using Jasmine.FastenerDepartment.Domain.Messaging.Models;
using Jasmine.FastenerDepartment.Domain.Messaging.Services;
using Jasmine.FastenerDepartment.Messaging.Factories;
using Jasmine.FastenerDepartment.Messaging.Models;

namespace Jasmine.FastenerDepartment.Messaging.Services;

internal class MessagesService : IMessagesService
{
    private readonly IMessageTemplateProvidersFactory _templateProvidersFactory;
    private readonly IMessagesTypeProvidersFactory _typeProvidersFactory;

    public MessagesService(
        IMessageTemplateProvidersFactory templateProvidersFactory,
        IMessagesTypeProvidersFactory typeProvidersFactory)
    {
        _templateProvidersFactory = templateProvidersFactory;
        _typeProvidersFactory = typeProvidersFactory;
    }

    public async Task SendAsync<T>(MessageRequest<T> request, CancellationToken cancellationToken = default)
        where T : MessageRequestData, new()
    {
        var templateProvider = _templateProvidersFactory.GetProvider(request.MessageTypeCode);
        var template = templateProvider.Render(request.TemplateTypeCode, request.Data);

        var messageProvider = _typeProvidersFactory.GetProvider(request.MessageTypeCode);
        var model = CreateSendModel(request, template);
        
        await messageProvider.SendAsync(model, cancellationToken);
    }

    private SendModel CreateSendModel<T>(MessageRequest<T> request, string template)
        where T : MessageRequestData, new()
    {
        return new SendModel
        {
            RecipientContact = request.RecipientContact,
            Title = request.Title,
            Content = template,
            Attachments = request.Attachments,
        };
    }
}
