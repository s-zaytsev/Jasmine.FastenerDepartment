using Jasmine.FastenerDepartment.Domain.Templates.Builders;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.OrderForm;
using Jasmine.FastenerDepartment.Templates.Pdf.Helpers;
using MigraDoc;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;

namespace Jasmine.FastenerDepartment.Templates.Pdf.Builders;

/// <summary>
/// Order form PDF template content builder.
/// </summary>
public class OrderFormPdfTemplateContentBuilder
    : OrderFormTemplateContentBuilder<Document, Table, Row, Cell>
{
    /// <summary>
    /// Creates builder.
    /// </summary>
    /// <param name="data">Order form template render data.</param>
    public OrderFormPdfTemplateContentBuilder(OrderFormTemplateRenderData data)
        : base(data)
    {
        GlobalFontSettings.UseWindowsFontsUnderWindows = true;
    }

    /// <summary>
    /// Sets the main container.
    /// </summary>
    protected override void SetMainContainer()
    {
        var document = PdfDocumentHelper.CreateMainDocument();
        MainContainer = document;
    }

    /// <summary>
    /// Converts the main container to the result.
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

        return new TemplateRenderResult(memoryStream, "application/pdf", "Order.pdf");
    }

    /// <summary>
    /// Appends a company name line.
    /// </summary>
    /// <param name="line">Company name line.</param>
    protected override void AppendCompanyNameLine(string line)
    {
        var companyName = PdfDocumentHelper.CreateParagraph(line, 12);
        MainContainer.LastSection.Add(companyName);
    }

    /// <summary>
    /// Appends a full name line.
    /// </summary>
    /// <param name="line">Full name line.</param>
    protected override void AppendFullNameLine(string line)
    {
        var fullName = PdfDocumentHelper.CreateParagraph(line, 12);
        MainContainer.LastSection.Add(fullName);
    }

    /// <summary>
    /// Appends an email line.
    /// </summary>
    /// <param name="line">Email line.</param>
    protected override void AppendEmailLine(string line)
    {
        var email = PdfDocumentHelper.CreateParagraph(line, 12);
        MainContainer.LastSection.Add(email);
    }

    /// <summary>
    /// Appends a city line.
    /// </summary>
    /// <param name="line">City line.</param>
    protected override void AppendCityLine(string line)
    {
        var city = PdfDocumentHelper.CreateParagraph(line, 12);
        MainContainer.LastSection.Add(city);
    }

    /// <summary>
    /// Appends an address line.
    /// </summary>
    /// <param name="line">Address line.</param>
    protected override void AppendAddressLine(string line)
    {
        var address = PdfDocumentHelper.CreateParagraph(line, 12);
        MainContainer.LastSection.Add(address);
    }

    /// <summary>
    /// Appends an INN line.
    /// </summary>
    /// <param name="line">INN line.</param>
    protected override void AppendInnLine(string line)
    {
        var inn = PdfDocumentHelper.CreateParagraph(line, 12);
        MainContainer.LastSection.Add(inn);
    }

    /// <summary>
    /// Appends a phone number line.
    /// </summary>
    /// <param name="line">Phone number line.</param>
    protected override void AppendPhoneNumberLine(string line)
    {
        var phone = PdfDocumentHelper.CreateParagraph(line, 12);
        MainContainer.LastSection.Add(phone);
    }

    /// <summary>
    /// Creates table.
    /// </summary>
    /// <returns>Table.</returns>
    protected override Table CreateTable()
    {
        var table = PdfDocumentHelper.CreateTable(
            Data.Content.TableColumnCodes, FixedWidthPercent, UnfixedWidthPercent);

        return table;
    }

    /// <summary>
    /// Appends table to result.
    /// </summary>
    /// <param name="table">Table.</param>
    protected override void AppendTable(Table table)
    {
        MainContainer.LastSection.Add(table);
    }

    /// <summary>
    /// Appends table group name.
    /// </summary>
    /// <param name="name">Name of group.</param>
    protected override void AppendTableGroupName(string name)
    {
        var topMargin = PdfDocumentHelper.CreateParagraph(" ", 15);
        MainContainer.LastSection.Add(topMargin);

        var groupName = PdfDocumentHelper.CreateTableGroupName(name);
        MainContainer.LastSection.Add(groupName);

        var bottomMargin = PdfDocumentHelper.CreateParagraph(" ", 5);
        MainContainer.LastSection.Add(bottomMargin);
    }

    /// <summary>
    /// Creates table row.
    /// </summary>
    /// <returns>Table row.</returns>
    protected override Row CreateTableRow()
    {
        var row = PdfDocumentHelper.CreateTableRow();
        return row;
    }

    /// <summary>
    /// Appends table row to table.
    /// </summary>
    /// <param name="table">Table.</param>
    /// <param name="row">Table row.</param>
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
    /// <param name="text">Cell text.</param>
    /// <param name="widthPercent">Width percent.</param>
    /// <returns>Table row cell.</returns>
    protected override Cell CreateTableRowCell(string text, string widthPercent)
    {
        var cell = PdfDocumentHelper.CreateTableCell(text);
        return cell;
    }

    /// <summary>
    /// Appends table row cell to table row.
    /// </summary>
    /// <param name="row">Table row.</param>
    /// <param name="cell">Table row cell.</param>
    protected override void AppendTableRowCell(Row row, Cell cell)
    {
        row.Cells.Add(cell);
    }
}
