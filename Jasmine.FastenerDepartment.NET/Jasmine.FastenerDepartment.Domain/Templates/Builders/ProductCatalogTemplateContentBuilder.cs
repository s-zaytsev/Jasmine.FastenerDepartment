using Jasmine.FastenerDepartment.Domain.Common.Builders;
using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.ProductCatalog;

namespace Jasmine.FastenerDepartment.Domain.Templates.Builders;

/// <summary>
/// Product catalog template content builder.
/// </summary>
/// <typeparam name="TContainer">Type of container.</typeparam>
/// <typeparam name="TTable">Type of table.</typeparam>
/// <typeparam name="TRow">Type of table row.</typeparam>
/// <typeparam name="TRowCell">Type of table row cell.</typeparam>
public abstract class ProductCatalogTemplateContentBuilder<TContainer, TTable, TRow, TRowCell>
    : TemplateBuilderBase<TContainer>
{
    private readonly LocalizedString _currency = new("rub", "руб");

    private readonly ProductCatalogTemplateRenderData _data;

    private readonly Dictionary<ProductCatalogTemplateContentTableColumnCode, int> _fixedWidthPercent = new()
    {
        { ProductCatalogTemplateContentTableColumnCode.Number, 15 },
        { ProductCatalogTemplateContentTableColumnCode.Type, 20 },
        { ProductCatalogTemplateContentTableColumnCode.Price, 15 },
    };

    private int _unfixedWidthPercent = 100;

    /// <summary>
    /// Creates builder.
    /// </summary>
    protected ProductCatalogTemplateContentBuilder(
        ProductCatalogTemplateRenderData data)
    {
        _data = data;

        foreach (var code in _data.Content.TableColumnCodes)
        {
            if (_fixedWidthPercent.TryGetValue(code, out var percent))
                _unfixedWidthPercent -= percent;
        }
    }

    /// <summary>
    /// Builds the template.
    /// </summary>
    /// <returns></returns>
    public override TemplateRenderResult Build()
    {
        SetMainContainer();

        if (!_data.Content.GroupByType)
            AppendUngroupedTable(_data.Products);
        else
            AppendGroupedTables(_data.Products);

        var result = ConvertMainContainerToResult();
        return result;
    }

    /// <summary>
    /// Creates table.
    /// </summary>
    /// <returns>Table.</returns>
    protected abstract TTable CreateTable();

    /// <summary>
    /// Appends table to result.
    /// </summary>
    /// <param name="table">Table.</param>
    protected abstract void AppendTable(TTable table);

    /// <summary>
    /// Appends table group name.
    /// </summary>
    /// <param name="name">Name of group.</param>
    protected abstract void AppendTableGroupName(string name);

    /// <summary>
    /// Creates table row.
    /// </summary>
    /// <returns>Table row.</returns>
    protected abstract TRow CreateTableRow();

    /// <summary>
    /// Appends table row to table.
    /// </summary>
    /// <param name="table">Table.</param>
    /// <param name="row">Table row.</param>
    protected abstract void AppendTableRow(TTable table, TRow row);

    /// <summary>
    /// Creates table row cell.
    /// </summary>
    /// <param name="text">Cell text.</param>
    /// <param name="widthPercent">Width percent.</param>
    /// <returns>Table row cell.</returns>
    protected abstract TRowCell CreateTableRowCell(string text, string widthPercent);

    /// <summary>
    /// Appends table row cell to table row.
    /// </summary>
    /// <param name="row">Table row.</param>
    /// <param name="cell">Table row cell.</param>
    protected abstract void AppendTableRowCell(TRow row, TRowCell cell);

    private void AppendUngroupedTable(
        IEnumerable<Product> products)
    {
        var table = CreateTable();
        foreach (var product in products)
        {
            var row = CreateTableRow(product);
            AppendTableRow(table, row);
        }

        AppendTable(table);
    }

    private void AppendGroupedTables(IEnumerable<Product> products)
    {
        var groupedByType = products
            .GroupBy(x => x.Type?.Name.Value ?? "Others")
            .ToDictionary(g => g.Key, g => g.ToList());

        foreach (var pair in groupedByType)
        {
            AppendTableGroupName(pair.Key);
            var table = CreateTable();
            foreach (var product in pair.Value)
            {
                var row = CreateTableRow(product);
                AppendTableRow(table, row);
            }

            AppendTable(table);
        }
    }

    private TRow CreateTableRow(Product product)
    {
        var row = CreateTableRow();

        foreach (var code in _data.Content.TableColumnCodes)
        {
            var width = _fixedWidthPercent.TryGetValue(code, out var value) ? value : _unfixedWidthPercent;
            var cell = GetTableRowCell(code, product, width.ToString());
            AppendTableRowCell(row, cell);
        }

        return row;
    }

    private TRowCell GetTableRowCell(
        ProductCatalogTemplateContentTableColumnCode code,
        Product product,
        string widthPercent)
    {
        return code switch
        {
            ProductCatalogTemplateContentTableColumnCode.Number =>
                CreateTableRowCell(product.Number.Value.ToString(), widthPercent),
            ProductCatalogTemplateContentTableColumnCode.Name =>
                CreateTableRowCell(product.Name.Value, widthPercent),
            ProductCatalogTemplateContentTableColumnCode.Type =>
                CreateTableRowCell(product.Type?.Name.Value ?? "", widthPercent),
            ProductCatalogTemplateContentTableColumnCode.Price =>
                CreateTableRowCell(
                    $"{product.Price.Value:F2} " +
                    $"{_currency.GetText(_data.LanguageCode)}/" +
                    $"{product.MeasurementUnit.ShortName.GetText(_data.LanguageCode)}",
                    widthPercent),
            _ => throw new NotSupportedException(),
        };
    }
}
