using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Jasmine.FastenerDepartment.Domain.Templates.Builders;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.ProductCatalog;
using Jasmine.FastenerDepartment.Templates.OfficeDocuments.Heplers;
using Table = DocumentFormat.OpenXml.Wordprocessing.Table;

namespace Jasmine.FastenerDepartment.Templates.OfficeDocuments.Builders;

/// <summary>
/// Product catalog Word template content builder.
/// </summary>
public class ProductCatalogWordTemplateContentBuilder :
    ProductCatalogTemplateContentBuilder<WordprocessingDocument, Table, TableRow, TableCell>
{
    /// <summary>
    /// Creates builder.
    /// </summary>
    public ProductCatalogWordTemplateContentBuilder(
        ProductCatalogTemplateRenderData data)
        : base(data)
    { }

    /// <summary>
    /// Sets the main container.
    /// </summary>
    protected override void SetMainContainer()
    {
        var memoryStream = new MemoryStream();

        var wordDocument = WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document, true);
        var mainPart = wordDocument.AddMainDocumentPart();
        mainPart.Document = new Document();
        mainPart.Document.AppendChild(new Body());

        MainContainer = wordDocument;
    }

    /// <summary>
    /// Converts the main container to result.
    /// </summary>
    /// <returns>Template render result.</returns>
    protected override TemplateRenderResult ConvertMainContainerToResult()
    {
        MainContainer.Save();

        var memoryStream = new MemoryStream();
        MainContainer.Clone(memoryStream);
        memoryStream.Position = 0;

        return new TemplateRenderResult(
            memoryStream,
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            $"Product catalog ({DateTime.Now:dd.MM.yyyy}).docx");
    }

    /// <summary>
    /// Creates table.
    /// </summary>
    /// <returns>Table.</returns>
    protected override Table CreateTable()
    {
        var table = WordDocumentHelper.CreateTable();
        return table;
    }

    /// <summary>
    /// Appends table to result.
    /// </summary>
    /// <param name="table">Table.</param>
    protected override void AppendTable(Table table)
    {
        var body = GetBody();
        body.AppendChild(table);
    }

    /// <summary>
    /// Appends table group name.
    /// </summary>
    /// <param name="name">Group name.</param>
    protected override void AppendTableGroupName(string name)
    {
        var paragraph = WordDocumentHelper.CreateParagraph(name, fontSize: "36");
        var body = GetBody();
        body.AppendChild(paragraph);
    }

    /// <summary>
    /// Creates table row.
    /// </summary>
    /// <returns>Table row.</returns>
    protected override TableRow CreateTableRow()
    {
        var tableRow = new TableRow();
        return tableRow;
    }

    /// <summary>
    /// Appends table row to table.
    /// </summary>
    /// <param name="table">Table.</param>
    /// <param name="row">Table row.</param>
    protected override void AppendTableRow(Table table, TableRow row)
    {
        table.AppendChild(row);
    }

    /// <summary>
    /// Creates table row cell.
    /// </summary>
    /// <param name="text">Cell text.</param>
    /// <param name="widthPercent">Width percent.</param>
    /// <returns>Table row cell.</returns>
    protected override TableCell CreateTableRowCell(string text, string widthPercent)
    {
        var cell = WordDocumentHelper.CreateTableCell(text, widthPercent, TableWidthUnitValues.Pct,"24");
        return cell;
    }

    /// <summary>
    /// Appends table row cell to table row.
    /// </summary>
    /// <param name="row">Table row.</param>
    /// <param name="cell">Table row cell.</param>
    protected override void AppendTableRowCell(TableRow row, TableCell cell)
    {
        row.AppendChild(cell);
    }

    private Body GetBody()
    {
        var body = MainContainer.MainDocumentPart.Document.Body;
        return body;
    }
}
