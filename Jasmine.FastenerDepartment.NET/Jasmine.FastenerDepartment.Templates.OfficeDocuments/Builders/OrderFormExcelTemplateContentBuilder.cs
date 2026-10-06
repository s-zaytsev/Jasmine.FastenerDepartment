using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Jasmine.FastenerDepartment.Domain.Templates.Builders;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.OrderForm;
using Jasmine.FastenerDepartment.Templates.OfficeDocuments.Helpers;

namespace Jasmine.FastenerDepartment.Templates.OfficeDocuments.Builders;

/// <summary>
/// Order form Excel template content builder.
/// </summary>
public class OrderFormExcelTemplateContentBuilder :
    OrderFormTemplateContentBuilder<SpreadsheetDocument, Table, Row, Cell>
{
    private uint _currentRowIndex = 1;
    private int _currentColumnIndex = 0;
    private readonly Dictionary<int, int> _columnMaxLengths = [];

    /// <summary>
    /// Create a builder.
    /// </summary>
    /// <param name="data">Order form template render data.</param>
    public OrderFormExcelTemplateContentBuilder(OrderFormTemplateRenderData data)
        : base(data)
    {}

    /// <summary>
    /// Sets the main container.
    /// </summary>
    protected override void SetMainContainer()
    {
        var memoryStream = new MemoryStream();

        var excelDocument = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook, true);
        var mainPart = excelDocument.AddWorkbookPart();
        mainPart.Workbook = new Workbook();

        var worksheetPart = mainPart.AddNewPart<WorksheetPart>();
        worksheetPart.Worksheet = new Worksheet(new SheetData());

        var sheets = excelDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
        var sheet = ExcelDocumentHelper.CreateSheet(excelDocument.WorkbookPart, worksheetPart, 1, "Order", sheets);

        sheets.Append(sheet);

        MainContainer = excelDocument;
    }

    /// <summary>
    /// Appends a company name line.
    /// </summary>
    /// <param name="line">Company name line.</param>
    protected override void AppendCompanyNameLine(string line)
    {
        AppendMergeCell(line);
    }

    /// <summary>
    /// Appends a full name line.
    /// </summary>
    /// <param name="line">Full name line.</param>
    protected override void AppendFullNameLine(string line)
    {
        AppendMergeCell(line);
    }

    /// <summary>
    /// Appends an email line.
    /// </summary>
    /// <param name="line">Email line.</param>
    protected override void AppendEmailLine(string line)
    {
        AppendMergeCell(line);
    }

    /// <summary>
    /// Appends a city line.
    /// </summary>
    /// <param name="line">City line.</param>
    protected override void AppendCityLine(string line)
    {
        AppendMergeCell(line);
    }

    /// <summary>
    /// Appends an address line.
    /// </summary>
    /// <param name="line">Address line.</param>
    protected override void AppendAddressLine(string line)
    {
        AppendMergeCell(line);
    }

    /// <summary>
    /// Appends an INN line.
    /// </summary>
    /// <param name="line">INN line.</param>
    protected override void AppendInnLine(string line)
    {
        AppendMergeCell(line);
    }

    /// <summary>
    /// Appends a phone number line.
    /// </summary>
    /// <param name="line">Phone number line.</param>
    protected override void AppendPhoneNumberLine(string line)
    {
        AppendMergeCell(line);
    }

    /// <summary>
    /// Creates table.
    /// </summary>
    /// <returns>Table.</returns>
    protected override Table CreateTable()
    {
        var table = ExcelDocumentHelper.CreateTable();
        return table;
    }

    /// <summary>
    /// Appends table to result.
    /// </summary>
    /// <param name="table">Table.</param>
    protected override void AppendTable(Table table)
    {
        var workbookPart = MainContainer.WorkbookPart;
        var worksheetPart = workbookPart.WorksheetParts.First();
        var worksheet = worksheetPart.Worksheet;
        var sheetData = worksheet.GetFirstChild<SheetData>();

        var rows = table.ChildElements.OfType<Row>().ToList();
        table.RemoveAllChildren();

        foreach (var row in rows)
        {
            sheetData.AppendChild(row);
        }
    }

    /// <summary>
    /// Appends table group name.
    /// </summary>
    /// <param name="name">Name of group.</param>
    protected override void AppendTableGroupName(string name)
    {
        _currentRowIndex++;
        var workbookPart = MainContainer.WorkbookPart;
        var worksheetPart = workbookPart.WorksheetParts.First();
        var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

        var groupRow = new Row() { RowIndex = _currentRowIndex };
        var groupCell = new Cell()
        {
            CellReference = $"A{_currentRowIndex}",
            DataType = CellValues.String,
            CellValue = new CellValue(name)
        };

        groupRow.AppendChild(groupCell);
        sheetData.AppendChild(groupRow);

        _currentRowIndex++;
    }

    /// <summary>
    /// Creates table row.
    /// </summary>
    /// <returns>Table row.</returns>
    protected override Row CreateTableRow()
    {
        var row = ExcelDocumentHelper.CreateTableRow(ref _currentRowIndex, ref _currentColumnIndex);
        return row;
    }

    /// <summary>
    /// Appends table row to table.
    /// </summary>
    /// <param name="table">Table.</param>
    /// <param name="row">Table row.</param>
    protected override void AppendTableRow(Table table, Row row)
    {
        table.AppendChild(row);
    }

    /// <summary>
    /// Creates table row cell.
    /// </summary>
    /// <param name="text">Cell text.</param>
    /// <param name="widthPercent">Width percent.</param>
    /// <returns>Table row cell.</returns>
    protected override Cell CreateTableRowCell(string text, string widthPercent)
    {
        var cell = ExcelDocumentHelper.CreateTableRowCell(text, _currentRowIndex, ref _currentColumnIndex);
        ExcelDocumentHelper.UpdateColumnsLength(text, _columnMaxLengths, _currentColumnIndex);

        return cell;
    }

    /// <summary>
    /// Appends table row cell to table row.
    /// </summary>
    /// <param name="row">Table row.</param>
    /// <param name="cell">Table row cell.</param>
    protected override void AppendTableRowCell(Row row, Cell cell)
    {
        row.AppendChild(cell);
    }

    /// <summary>
    /// Converts the main container to the result.
    /// </summary>
    /// <returns>Template render result.</returns>
    protected override TemplateRenderResult ConvertMainContainerToResult()
    {
        var workbookPart = MainContainer.WorkbookPart;
        var worksheetPart = workbookPart.WorksheetParts.First();
        var worksheet = worksheetPart.Worksheet;
        var sheetData = worksheet.GetFirstChild<SheetData>();

        ExcelDocumentHelper.ApplyColumnsWidth(_columnMaxLengths, worksheet, sheetData);

        MainContainer.Save();

        var memoryStream = new MemoryStream();
        MainContainer.Clone(memoryStream);
        memoryStream.Position = 0;

        MainContainer.Dispose();

        return new TemplateRenderResult(
            memoryStream,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"Order ({DateTime.Now:dd.MM.yyyy}).xlsx");
    }

    private void AppendMergeCell(string text)
    {
        var workbookPart = MainContainer.WorkbookPart;
        var worksheetPart = workbookPart.WorksheetParts.First();
        var worksheet = worksheetPart.Worksheet;
        var sheetData = worksheet.GetFirstChild<SheetData>();

        var textRow = new Row() { RowIndex = _currentRowIndex };

        var textCell = new Cell()
        {
            CellReference = $"A{_currentRowIndex}",
            DataType = CellValues.String,
            CellValue = new CellValue(text ?? string.Empty)
        };

        textRow.AppendChild(textCell);
        sheetData.AppendChild(textRow);

        var mergeCells = worksheet.GetFirstChild<MergeCells>();
        if (mergeCells == null)
        {
            mergeCells = new MergeCells();

            var tableParts = worksheet.GetFirstChild<TableParts>();
            if (tableParts != null)
            {
                worksheet.InsertBefore(mergeCells, tableParts);
            }
            else
            {
                worksheet.InsertAfter(mergeCells, sheetData);
            }
        }

        var mergeReference = $"A{_currentRowIndex}:E{_currentRowIndex}";
        mergeCells.AppendChild(new MergeCell() { Reference = mergeReference });

        _currentRowIndex++;
    }
}
