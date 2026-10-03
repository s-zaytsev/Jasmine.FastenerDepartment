using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.Domain.Orders.Models;

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
    public MessageTypeCode MessageType { get; set; }

    /// <summary>
    /// Company identifier.
    /// </summary>
    public Guid? CompanyId { get; set; }

    /// <summary>
    /// Template identifier.
    /// </summary>
    public Guid TemplateId { get; set; }

    /// <summary>
    /// Collection of template format codes.
    /// </summary>
    public ICollection<TemplateFormatCode> Formats { get; set; }

    /// <summary>
    /// Attachments.
    /// </summary>
    public ICollection<FileModel> Attachments { get; set; } 
}
