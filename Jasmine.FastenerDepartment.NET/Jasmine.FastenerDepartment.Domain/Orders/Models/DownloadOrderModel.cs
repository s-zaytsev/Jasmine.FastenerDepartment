using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.Domain.Orders.Models;

/// <summary>
/// Download order model.
/// </summary>
public class DownloadOrderModel
{
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
