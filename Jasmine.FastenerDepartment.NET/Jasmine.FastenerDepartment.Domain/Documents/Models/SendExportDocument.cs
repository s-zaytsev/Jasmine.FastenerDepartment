using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.Domain.Documents.Models;

/// <summary>
/// Send document.
/// </summary>
public class SendExportDocument
{
    /// <summary>
    /// Template identifier.
    /// </summary>
    public Guid TemplateId { get; set; }

    /// <summary>
    /// Format code.
    /// </summary>
    public TemplateFormatCode FormatCode { get; set; }

    /// <summary>
    /// Message type.
    /// </summary>
    public MessageTypeCode MessageTypeCode { get; set; }

    /// <summary>
    /// Recipient contact.
    /// </summary>
    public string RecipientContact { get; set; }
}
