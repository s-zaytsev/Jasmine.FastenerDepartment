using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.ProductCatalog;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Templates.Content;

/// <summary>
/// Product catalog template content.
/// </summary>
/// <param name="GroupByType">Whether the content groups product by type.</param>
/// <param name="TableColumns">The collection of table column codes.</param>
public record ProductCatalogTemplateContentDto(
    bool GroupByType,
    IEnumerable<TemplateContentTableColumnDto> TableColumns)
    : TemplateContentDto;