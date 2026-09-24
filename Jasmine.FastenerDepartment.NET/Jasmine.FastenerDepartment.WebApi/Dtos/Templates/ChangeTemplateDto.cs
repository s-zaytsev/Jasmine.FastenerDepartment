using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.WebApi.Converters;
using Jasmine.FastenerDepartment.WebApi.Dtos.Templates.Content;
using System.Text.Json.Serialization;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Templates;

/// <summary>
/// Change template model.
/// </summary>
/// <param name="Name">Name.</param>
/// <param name="TypeCode">Template type code.</param>
/// <param name="Content">Content.</param>
[JsonConverter(typeof(ChangeTemplateDtoConverter))]
public record ChangeTemplateDto(
    string Name,
    TemplateTypeCode TypeCode,
    TemplateContentDto Content);
