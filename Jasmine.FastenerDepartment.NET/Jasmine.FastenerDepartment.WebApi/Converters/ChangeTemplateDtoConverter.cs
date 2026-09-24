using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.WebApi.Dtos.Templates;
using Jasmine.FastenerDepartment.WebApi.Dtos.Templates.Content;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Jasmine.FastenerDepartment.WebApi.Converters;

internal class ChangeTemplateDtoConverter : JsonConverter<ChangeTemplateDto>
{
    public override ChangeTemplateDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonNode = JsonNode.Parse(ref reader);
        if (jsonNode is not JsonObject jsonObject)
        {
            return null;
        }

        var name = jsonObject["name"]?.GetValue<string>();
        var typeCodeValue = jsonObject["typeCode"].GetValue<int>();
        var typeCode = (TemplateTypeCode)typeCodeValue;

        TemplateContentDto content = null;
        var contentNode = jsonObject["content"];

        if (contentNode != null)
        {
            Type targetContentType = typeCode switch
            {
                TemplateTypeCode.ProductCatalog => typeof(ProductCatalogTemplateContentDto),
                TemplateTypeCode.OrderForm => typeof(OrderFormTemplateContentDto),
                _ => throw new JsonException($"Type code {typeCode} not supported.")
            };

            content = (TemplateContentDto)contentNode.Deserialize(targetContentType, options);
        }

        return new ChangeTemplateDto(
            name,
            typeCode,
            content);
    }

    public override void Write(Utf8JsonWriter writer, ChangeTemplateDto value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("name", value.Name);
        writer.WriteNumber("typeCode", (int)value.TypeCode);
        writer.WritePropertyName("content");
        JsonSerializer.Serialize(writer, value.Content, value.Content?.GetType() ?? typeof(TemplateContentDto), options);
        writer.WriteEndObject();
    }
}
