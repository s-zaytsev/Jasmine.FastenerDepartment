using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Documents;

/// <summary>
/// Download export document model.
/// </summary>
/// <param name="TemplateId">Template identifier.</param>
/// <param name="FormatCode">Format code.</param>
public record DownloadExportDocumentDto(
    Guid TemplateId,
    TemplateFormatCode FormatCode);
