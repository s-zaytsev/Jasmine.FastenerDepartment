using Jasmine.FastenerDepartment.Messaging.Models;

namespace Jasmine.FastenerDepartment.Messaging.Services;

/// <summary>
/// Message service.
/// </summary>
public interface IMessageService
{
    /// <summary>
    /// Sends a message.
    /// </summary>
    /// <param name="request">Message request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Recepient address.</returns>
    Task<string> SendAsync(MessageRequest request, CancellationToken cancellationToken = default);
}
