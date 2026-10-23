using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Orders;

/// <summary>
/// Order form render request.
/// </summary>
/// <param name="TemplateId">Template identifier.</param>
/// <param name="FormatCode">Format code.</param>
/// <param name="CompanyId">Company identifier.</param>
public record RenderOrderFormDto(
    Guid TemplateId,
    TemplateFormatCode FormatCode,
    Guid? CompanyId);
