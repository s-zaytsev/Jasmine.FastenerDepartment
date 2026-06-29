using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Common.Exceptions;

/// <summary>
/// Domain exception.
/// </summary>
public class DomainException : InvalidOperationException
{
    /// <summary>
    /// Exception code.
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// User message.
    /// </summary>
    public LocalizedString UserMessage { get; set; }

    /// <summary>
    /// Creates exception.
    /// </summary>
    /// <param name="code">Exception code.</param>
    /// <param name="message">Exception message.</param>
    /// <param name="userMessage">Exception user message.</param>
    /// <param name="innerException">Inner exception.</param>
    public DomainException(
        int code,
        string message,
        LocalizedString userMessage = null,
        Exception innerException = null)
        : base(message ?? userMessage.En, innerException)
    {
        Code = code;
        UserMessage = userMessage;
    }
}
