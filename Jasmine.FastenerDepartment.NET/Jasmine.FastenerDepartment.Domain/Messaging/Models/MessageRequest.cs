using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.Domain.Messaging.Models;

/// <summary>
/// Message request.
/// </summary>
public class MessageRequest
{
    /// <summary>
    /// Message type.
    /// </summary>
    public MessageTypeCode MessageTypeCode { get; set; }

    /// <summary>
    /// Template type code.
    /// </summary>
    public TemplateTypeCode TemplateTypeCode { get; set; }

    /// <summary>
    /// Recipient contact.
    /// </summary>
    public string RecipientContact { get; set; }

    /// <summary>
    /// Title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Request data.
    /// </summary>
    public MessageRequestData Data { get; set; }

    /// <summary>
    /// Attachments.
    /// </summary>
    public List<FileModel> Attachments { get; set; }

    /// <summary>
    /// Creates a message request.
    /// </summary>
    public MessageRequest(
        MessageTypeCode messageTypeCode,
        TemplateTypeCode templateTypeCode,
        string recipientContact,
        string title,
        MessageRequestData data,
        List<FileModel> attachments)
    {
        MessageTypeCode = messageTypeCode;
        TemplateTypeCode = templateTypeCode;
        RecipientContact = recipientContact;
        Title = title;
        Data = data;
        Attachments = attachments;
    }
}
