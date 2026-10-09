using Jasmine.FastenerDepartment.Domain.Templates.Builders;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.ProductCatalog;
using Jasmine.FastenerDepartment.Templates.Pdf.Helpers;
using MigraDoc;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;

namespace Jasmine.FastenerDepartment.Templates.Pdf.Builders;

/// <summary>
/// Product catalog PDF template content builder.
/// </summary>
public class ProductCatalogPdfTemplateContentBuilder
    : ProductCatalogTemplateContentBuilder<Document, Table, Row, Cell>
{
    /// <summary>
    /// Creates builder.
    /// </summary>
    /// <param name="data">Product catalog template render data.</param>
    public ProductCatalogPdfTemplateContentBuilder(ProductCatalogTemplateRenderData data)
        : base(data)
    {
        GlobalFontSettings.UseWindowsFontsUnderWindows = true;
    }

    /// <summary>
    /// Sets main container.
    /// </summary>
    protected override void SetMainContainer()
    {
        var document = PdfDocumentHelper.CreateMainDocument();
        MainContainer = document;
    }

    /// <summary>
    /// Converts the main container to result.
    /// </summary>
    /// <returns>Template render result.</returns>
    protected override TemplateRenderResult ConvertMainContainerToResult()
    {
        PredefinedFontsAndChars.ErrorFontName = "Arial";

        var renderer = new PdfDocumentRenderer
        {
            Document = MainContainer
        };

        renderer.RenderDocument();

        var memoryStream = new MemoryStream();
        renderer.PdfDocument.Save(memoryStream, false);

        return new TemplateRenderResult(memoryStream, "application/pdf", "Product catalog.pdf");
    }

    /// <summary>
    /// Create table.
    /// </summary>
    /// <returns>Table.</returns>
    protected override Table CreateTable()
    {
        var table = PdfDocumentHelper.CreateTable(
            Data.Content.TableColumnCodes, FixedWidthPercent, UnfixedWidthPercent);

        return table;
    }

    /// <summary>
    /// Appends table.
    /// </summary>
    /// <param name="table">Table.</param>
    protected override void AppendTable(Table table)
    {
        MainContainer.LastSection.Add(table);
    }

    /// <summary>
    /// Appends table group name.
    /// </summary>
    /// <param name="name">Table group name.</param>
    protected override void AppendTableGroupName(string name)
    {
        MainContainer.LastSection.AddPageBreak();
        var groupName = PdfDocumentHelper.CreateTableGroupName(name);
        MainContainer.LastSection.Add(groupName);
        var margin = PdfDocumentHelper.CreateParagraph(" ", 5);
        MainContainer.LastSection.Add(margin);
    }

    /// <summary>
    /// Create table row.
    /// </summary>
    /// <returns>Row.</returns>
    protected override Row CreateTableRow()
    {
        var row = PdfDocumentHelper.CreateTableRow();
        return row;
    }

    /// <summary>
    /// Appends table row.
    /// </summary>
    /// <param name="table">Table.</param>
    /// <param name="row">Row.</param>
    protected override void AppendTableRow(Table table, Row row)
    {
        if (table.Rows.Count % 2 != 0)
        {
            row.Shading.Color = Color.Parse("#F5F7FA");
        }

        table.Rows.Add(row);
    }

    /// <summary>
    /// Creates table row cell.
    /// </summary>
    /// <param name="text">Text.</param>
    /// <param name="widthPercent">Width percent.</param>
    /// <returns>Cell.</returns>
    protected override Cell CreateTableRowCell(string text, string widthPercent)
    {
        var cell = PdfDocumentHelper.CreateTableCell(text);
        return cell;
    }

    /// <summary>
    /// Appends table row cell.
    /// </summary>
    /// <param name="row">Row.</param>
    /// <param name="cell">Cell.</param>
    protected override void AppendTableRowCell(Row row, Cell cell)
    {
        row.Cells.Add(cell);
    }
}
