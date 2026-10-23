using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.Domain.Documents.Models;

/// <summary>
/// Download export document model.
/// </summary>
public class DownloadExportDocument
{
    /// <summary>
    /// Template identifier.
    /// </summary>
    public Guid TemplateId { get; set; }

    /// <summary>
    /// Format code.
    /// </summary>
    public TemplateFormatCode FormatCode { get; set; }
}
