using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Orders;

/// <summary>
/// Download order model.
/// </summary>
/// <param name="TemplateId">Template identifier.</param>
/// <param name="CompanyId">Company identifier.</param>
/// <param name="Formats">Collection of template formats.</param>
/// <param name="Attachments">Attachments.</param>
public record DownloadOrderDto(
    Guid TemplateId,
    Guid? CompanyId,
    List<TemplateFormatCode> Formats,
    List<IFormFile> Attachments);
