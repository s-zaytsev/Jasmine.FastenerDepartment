using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.OrderForm;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.ProductCatalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Jasmine.FastenerDepartment.EF.Configurations;

class TemplateConfiguration : IEntityTypeConfiguration<Template>
{
    private static readonly JsonSerializerOptions JsonOptions = CreateTemplateJsonOptions();

    public void Configure(EntityTypeBuilder<Template> builder)
    {
        builder.ToTable("Templates");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Name, o =>
        {
            o.Property(x => x.Value).HasColumnName("Title").HasColumnType("varchar(500)").IsRequired();
            o.HasIndex(x => x.Value).IsUnique();
        });

        builder
            .HasOne(x => x.Type)
            .WithMany()
            .HasForeignKey(x => x.TypeCode)
            .OnDelete(DeleteBehavior.Restrict);

        var contentConverter = new ValueConverter<TemplateContent, string>(
            v => JsonSerializer.Serialize(v, JsonOptions),
            v => JsonSerializer.Deserialize<TemplateContent>(v, JsonOptions)
        );

        var contentComparer = new ValueComparer<TemplateContent>(
            (c1, c2) => JsonSerializer.Serialize(c1, JsonOptions) == JsonSerializer.Serialize(c2, JsonOptions),
            c => c == null ? 0 : JsonSerializer.Serialize(c, JsonOptions).GetHashCode(),
            c => JsonSerializer.Deserialize<TemplateContent>(JsonSerializer.Serialize(c, JsonOptions), JsonOptions)
        );

        builder.Property(t => t.Content)
            .HasConversion(contentConverter, contentComparer)
            .HasColumnType("jsonb");
    }

    private static JsonSerializerOptions CreateTemplateJsonOptions()
    {
        var resolver = new DefaultJsonTypeInfoResolver();

        resolver.Modifiers.Add(jsonTypeInfo =>
        {
            // 1. Программно настраиваем полиморфизм для базового класса TemplateContent
            if (jsonTypeInfo.Type == typeof(TemplateContent))
            {
                jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                {
                    TypeDiscriminatorPropertyName = "$type",
                    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
                    DerivedTypes =
                    {
                        new JsonDerivedType(typeof(ProductCatalogTemplateContent), "product_list"),
                        new JsonDerivedType(typeof(OrderFormTemplateContent), "order_request")
                    }
                };
            }

            // 2. Разрешаем использовать protected/non-public конструкторы при десериализации
            if (typeof(TemplateContent).IsAssignableFrom(jsonTypeInfo.Type) && !jsonTypeInfo.Type.IsAbstract)
            {
                jsonTypeInfo.CreateObject = () =>
                    RuntimeHelpers.GetUninitializedObject(jsonTypeInfo.Type);
            }
        });

        return new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            TypeInfoResolver = resolver
        };
    }
}
