using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Documents;

/// <summary>
/// Send export document model.
/// </summary>
/// <param name="RecipientContact">Recipient contact.</param>
/// <param name="TemplateId">Template identifier.</param>
/// <param name="FormatCode">Template format code.</param>
public record SendExportDocumentDto(
    string RecipientContact,
    Guid TemplateId,
    TemplateFormatCode FormatCode);
