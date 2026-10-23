using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Messaging.Models;

/// <summary>
/// Message request data.
/// </summary>
public abstract class MessageRequestData
{
    /// <summary>
    /// Language code.
    /// </summary>
    public LanguageCode? LanguageCode { get; set; }
}
