using Jasmine.FastenerDepartment.Domain.Messaging.Models;

namespace Jasmine.FastenerDepartment.Domain.Messaging.Services;

/// <summary>
/// Message service.
/// </summary>
public interface IMessagesService
{
    /// <summary>
    /// Sends a message.
    /// </summary>
    /// <param name="request">Message request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task SendAsync(MessageRequest request, CancellationToken cancellationToken = default);
}
