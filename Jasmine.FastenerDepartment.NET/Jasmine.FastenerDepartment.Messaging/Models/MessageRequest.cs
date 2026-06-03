using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Messaging.Models;

/// <summary>
/// Message request.
/// </summary>
public class MessageRequest
{
    /// <summary>
    /// Message type.
    /// </summary>
    public MessageType Type { get; set; }

    /// <summary>
    /// Recipient contact.
    /// </summary>
    public string RecipientContact { get; set; }

    /// <summary>
    /// Title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Content.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Attachments.
    /// </summary>
    public ICollection<FileModel> Attachments { get; set; }
}
