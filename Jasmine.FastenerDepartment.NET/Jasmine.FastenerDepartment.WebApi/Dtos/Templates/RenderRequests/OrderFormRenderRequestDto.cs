using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Templates.RenderRequests;

/// <summary>
/// Order form render request.
/// </summary>
/// <param name="Id">Template identifier.</param>
/// <param name="FormatCode">Format code.</param>
/// <param name="OrderId">Order identifier.</param>
/// <param name="CompanyId">Company identifier.</param>
public record OrderFormRenderRequestDto(
    Guid Id,
    TemplateFormatCode FormatCode,
    Guid OrderId,
    Guid? CompanyId);
