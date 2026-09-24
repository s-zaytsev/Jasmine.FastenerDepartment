using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.WebApi.Dtos.Templates;
using Jasmine.FastenerDepartment.WebApi.Dtos.Templates.Content;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Jasmine.FastenerDepartment.WebApi.Converters;

internal class TemplateDtoConverter : JsonConverter<TemplateDto>
{
    public override TemplateDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonNode = JsonNode.Parse(ref reader);
        if (jsonNode is not JsonObject jsonObject) return null;

        Guid id = jsonObject["id"].GetValue<Guid>();
        string name = jsonObject["name"]?.GetValue<string>();

        var typeNode = jsonObject["type"];
        TemplateTypeDto templateType = typeNode?.Deserialize<TemplateTypeDto>(options);

        TemplateContentDto content = null;
        var contentNode = jsonObject["content"];

        if (contentNode != null && templateType != null)
        {
            Type targetContentType = templateType.Id switch
            {
                TemplateTypeCode.ProductCatalog => typeof(ProductCatalogTemplateContentDto),
                TemplateTypeCode.OrderForm => typeof(OrderFormTemplateContentDto),
                _ => throw new JsonException($"Type code {templateType.Id} not supported.")
            };

            content = (TemplateContentDto)contentNode.Deserialize(targetContentType, options);
        }

        return new TemplateDto(
            id,
            name,
            templateType,
            content);
    }

    public override void Write(Utf8JsonWriter writer, TemplateDto value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStartObject();

        writer.WriteString("id", value.Id.ToString());
        writer.WriteString("name", value.Name);
        writer.WritePropertyName("type");
        JsonSerializer.Serialize(writer, value.Type, options);
        writer.WritePropertyName("content");

        if (value.Content != null)
            JsonSerializer.Serialize(writer, value.Content, value.Content.GetType(), options);
        else
            writer.WriteNullValue();

        writer.WriteEndObject();
    }
}
