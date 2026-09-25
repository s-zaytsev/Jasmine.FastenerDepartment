using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Templates.RenderRequests;

/// <summary>
/// Product catalog render request.
/// </summary>
/// <param name="TemplateId">Template identifier,</param>
/// <param name="FormatCode">Format code.</param>
public record ProductCatalogRenderRequestDto(
    Guid TemplateId,
    TemplateFormatCode FormatCode);
