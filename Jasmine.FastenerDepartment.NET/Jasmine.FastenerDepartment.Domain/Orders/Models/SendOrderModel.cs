using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Products.Models;

/// <summary>
/// Send order model.
/// </summary>
public class SendOrderModel
{
    /// <summary>
    /// Recipient contact.
    /// </summary>
    public string RecipientContact { get; set; }

    /// <summary>
    /// Message type.
    /// </summary>
    public MessageType MessageType { get; set; }

    /// <summary>
    /// Attachments.
    /// </summary>
    public ICollection<FileModel> Attachments { get; set; } 
}
