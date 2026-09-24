using Jasmine.FastenerDepartment.Domain.Templates.Builders;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.ProductCatalog;
using Jasmine.FastenerDepartment.Templates.Html.Heplers;
using System.Text;

namespace Jasmine.FastenerDepartment.Templates.Html.Builders;

/// <summary>
/// Product catalog HTML template content builder.
/// </summary>
public class ProductCatalogHtmlTemplateContentBuilder
    : ProductCatalogTemplateContentBuilder<StringBuilder, StringBuilder, StringBuilder, string>
{
    /// <summary>
    /// Creates builder.
    /// </summary>
    public ProductCatalogHtmlTemplateContentBuilder(
        ProductCatalogTemplateRenderData data)
        : base(data)
    { }

    /// <summary>
    /// Sets main container.
    /// </summary>
    protected override void SetMainContainer()
    {
        MainContainer = new StringBuilder("<div></div>");
    }

    /// <summary>
    /// Converts the main container to result.
    /// </summary>
    /// <returns>Template render result.</returns>
    protected override TemplateRenderResult ConvertMainContainerToResult()
    {
        return new TemplateRenderResult(
            new MemoryStream(Encoding.UTF8.GetBytes(MainContainer.ToString())),
            "text/html",
            $"Product catalog ({DateTime.Now:dd.MM.yyyy}).html");
    }

    /// <summary>
    /// Creates table.
    /// </summary>
    /// <returns>Table.</returns>
    protected override StringBuilder CreateTable()
    {
        var table = new StringBuilder("<table style=\"width: 100%; border-collapse: collapse;\"><tbody></tbody></table>");
        return table;
    }

    /// <summary>
    /// Appends table to result.
    /// </summary>
    /// <param name="table">Table.</param>
    protected override void AppendTable(StringBuilder table)
    {
        var lastTag = "</div>";
        HtmlDocumentHelper.InsertToEnd(MainContainer, table.ToString(), lastTag);
    }

    /// <summary>
    /// Appends table group name.
    /// </summary>
    /// <param name="name">Name</param>
    protected override void AppendTableGroupName(string name)
    {
        var lastTag = "</div>";
        name = $"<h3 style=\"margin: 12px 0 6px 0; text-align: center;\">{name}</h3>";
        HtmlDocumentHelper.InsertToEnd(MainContainer, name, lastTag);
    }

    /// <summary>
    /// Creates table row.
    /// </summary>
    /// <returns>Table row.</returns>
    protected override StringBuilder CreateTableRow()
    {
        var tableRow = new StringBuilder("<tr></tr>");
        return tableRow;
    }

    /// <summary>
    /// Appends table row to table.
    /// </summary>
    /// <param name="table">Table.</param>
    /// <param name="row">Table row.</param>
    protected override void AppendTableRow(StringBuilder table, StringBuilder row)
    {
        var lastTag = "</tbody>";
        HtmlDocumentHelper.InsertToEnd(table, row.ToString(), lastTag);
    }

    /// <summary>
    /// Creates table row cell.
    /// </summary>
    /// <param name="text">Cell text.</param>
    /// <param name="widthPercent">Width percent.</param>
    /// <returns>Table row cell.</returns>
    protected override string CreateTableRowCell(string text, string widthPercent)
    {
        var cell = $"<td style=\"width: {widthPercent}%; border: 1px solid #dddddd; text-align: left; padding: 8px;\">{text}</td>";
        return cell;
    }

    /// <summary>
    /// Appends table row cell to table row.
    /// </summary>
    /// <param name="row">Table row.</param>
    /// <param name="cell">Table row cell.</param>
    protected override void AppendTableRowCell(StringBuilder row, string cell)
    {
        var lastTag = "</tr>";
        HtmlDocumentHelper.InsertToEnd(row, cell, lastTag);
    }
}
