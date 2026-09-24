using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Templates.Content;

/// <summary>
/// Template content table column group.
/// </summary>
/// <param name="typeCode">Template type code.</param>
/// <param name="columns">Columns.</param>
public record TemplateContentTableColumnGroupDto(
    TemplateTypeCode typeCode,
    ICollection<TemplateContentTableColumnDto> columns);
