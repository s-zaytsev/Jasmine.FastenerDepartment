using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Templates;

/// <summary>
/// Templates query.
/// </summary>
/// <param name="TypeCodes">Template type codes.</param>
public record TemplatesQueryDto(ICollection<TemplateTypeCode> TypeCodes);
