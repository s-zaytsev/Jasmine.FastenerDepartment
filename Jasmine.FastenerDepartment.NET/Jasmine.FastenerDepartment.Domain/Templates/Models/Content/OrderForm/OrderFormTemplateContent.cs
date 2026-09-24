namespace Jasmine.FastenerDepartment.Domain.Templates.Models.Content.OrderForm;

/// <summary>
/// Order form template content.
/// </summary>
public class OrderFormTemplateContent : TemplateContent
{
    /// <summary>
    /// Whether the content has company data.
    /// </summary>
    public bool HasCompanyData { get; init; }

    /// <summary>
    /// Whether the content groups product by type.
    /// </summary>
    public bool GroupByType { get; init; }

    /// <summary>
    /// Collection of table column codes.
    /// </summary>
    public IEnumerable<OrderFormTemplateContentTableColumnCode> TableColumnCodes { get; init; } = [];

    /// <summary>
    /// Creates a content.
    /// </summary>
    /// <param name="hasCompanyData">Whether the content has company data.</param>
    /// <param name="groupByType">Whether the content groups product by type.</param>
    /// <param name="tableColumnCodes">Collection of table column codes.</param>
    public OrderFormTemplateContent(
        bool hasCompanyData,
        bool groupByType,
        IEnumerable<OrderFormTemplateContentTableColumnCode> tableColumnCodes)
    {
        HasCompanyData = hasCompanyData;
        GroupByType = groupByType;
        TableColumnCodes = tableColumnCodes ?? [];
    }
}
