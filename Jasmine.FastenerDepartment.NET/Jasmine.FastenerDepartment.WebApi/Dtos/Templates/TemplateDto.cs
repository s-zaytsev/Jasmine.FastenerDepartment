using Jasmine.FastenerDepartment.WebApi.Converters;
using Jasmine.FastenerDepartment.WebApi.Dtos.Templates.Content;
using System.Text.Json.Serialization;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Templates;

/// <summary>
/// Template.
/// </summary>
/// <param name="Id">Identifier.</param>
/// <param name="Name">Name.</param>
/// <param name="Type">Type.</param>
/// <param name="Content">Content.</param>
[JsonConverter(typeof(TemplateDtoConverter))]
public record TemplateDto(
    Guid Id,
    string Name,
    TemplateTypeDto Type,
    TemplateContentDto Content);
