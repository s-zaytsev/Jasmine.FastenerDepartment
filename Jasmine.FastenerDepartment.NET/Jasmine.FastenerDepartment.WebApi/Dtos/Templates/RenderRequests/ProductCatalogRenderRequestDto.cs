using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Templates.RenderRequests;

/// <summary>
/// Product catalog render request.
/// </summary>
/// <param name="Id">Template identifier,</param>
/// <param name="FormatCode">Format code.</param>
public record ProductCatalogRenderRequestDto(
    Guid Id,
    TemplateFormatCode FormatCode);
