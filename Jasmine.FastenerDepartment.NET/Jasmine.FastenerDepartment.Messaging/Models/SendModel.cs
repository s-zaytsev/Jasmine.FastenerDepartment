using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Messaging.Models;

internal class SendModel
{
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
    public List<FileModel> Attachments { get; set; }
}
