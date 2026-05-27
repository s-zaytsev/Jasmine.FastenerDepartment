using Jasmine.FastenerDepartment.Messaging.Models;
using Jasmine.FastenerDepartment.Messaging.Services;

namespace Jasmine.FastenerDepartment.Messaging.Factories;

/// <summary>
/// Message services factory.
/// </summary>
public interface IMessageServicesFactory
{
    /// <summary>
    /// Returns a message service.
    /// </summary>
    /// <param name="type">Message type.</param>
    /// <returns>Message service.</returns>
    IMessageService GetService(MessageType type);
}
