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
    public string UserMessage { get; set; }

    /// <summary>
    /// Creates exception.
    /// </summary>
    /// <param name="code">Exception code.</param>
    /// <param name="message">Exception message.</param>
    /// <param name="userMessage">Exception user message.</param>
    /// <param name="innerException">Inner exception.</param>
    public DomainException(
        int code, string message, string userMessage = null, Exception innerException = null)
        : base(message ?? userMessage, innerException)
    {
        Code = code;
        UserMessage = userMessage;
    }
}
