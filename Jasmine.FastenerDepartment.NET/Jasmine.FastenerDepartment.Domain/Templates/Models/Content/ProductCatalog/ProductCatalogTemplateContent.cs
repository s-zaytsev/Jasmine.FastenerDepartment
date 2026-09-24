namespace Jasmine.FastenerDepartment.Domain.Templates.Models.Content.ProductCatalog;

/// <summary>
/// Product catalog template content.
/// </summary>
public class ProductCatalogTemplateContent : TemplateContent
{
    /// <summary>
    /// Whether the content groups products by type.
    /// </summary>
    public bool GroupByType { get; init; }

    /// <summary>
    /// Collection of table column codes.
    /// </summary>
    public IEnumerable<ProductCatalogTemplateContentTableColumnCode> TableColumnCodes { get; init; } = [];

    /// <summary>
    /// Creates catalog template content.
    /// </summary>
    /// <param name="groupByType">Whether the content groups products by type.</param>
    /// <param name="tableColumnCodes">Collection of table column codes.</param>
    public ProductCatalogTemplateContent(
        bool groupByType,
        IEnumerable<ProductCatalogTemplateContentTableColumnCode> tableColumnCodes)
    {
        GroupByType = groupByType;
        TableColumnCodes = tableColumnCodes ?? [];
    }
}
