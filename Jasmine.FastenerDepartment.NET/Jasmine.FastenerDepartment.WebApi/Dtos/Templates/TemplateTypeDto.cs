using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Templates;

/// <summary>
/// Template type.
/// </summary>
/// <param name="Id">Identifier.</param>
/// <param name="Name">Name.</param>
public record TemplateTypeDto(
    TemplateTypeCode Id,
    string Name);
