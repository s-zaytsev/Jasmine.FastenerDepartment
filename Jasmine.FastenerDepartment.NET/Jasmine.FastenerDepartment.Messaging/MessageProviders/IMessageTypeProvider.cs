using Jasmine.FastenerDepartment.Messaging.Models;

namespace Jasmine.FastenerDepartment.Messaging.MessageProviders;

internal interface IMessageTypeProvider
{
    Task SendAsync(SendModel model, CancellationToken cancellationToken);
}
