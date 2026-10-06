using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Jasmine.FastenerDepartment.Domain.Templates.Builders;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.ProductCatalog;
using Jasmine.FastenerDepartment.Templates.OfficeDocuments.Helpers;

namespace Jasmine.FastenerDepartment.Templates.OfficeDocuments.Builders;

/// <summary>
/// Product catalog excel template content builder.
/// </summary>
public class ProductCatalogExcelTemplateContentBuilder :
    ProductCatalogTemplateContentBuilder<SpreadsheetDocument, Table, Row, Cell>
{
    private uint _currentRowIndex = 1;
    private int _currentColumnIndex = 0;
    private uint _nextSheetId = 1;

    private SheetData _currentSheetData;
    private WorksheetPart _currentWorksheetPart;

    private readonly Dictionary<uint, Dictionary<int, int>> _sheetColumnWidths = [];

    /// <summary>
    /// Creates builder.
    /// </summary>
    public ProductCatalogExcelTemplateContentBuilder(
        ProductCatalogTemplateRenderData data)
        : base(data)
    { }

    /// <summary>
    /// Sets the main container.
    /// </summary>
    protected override void SetMainContainer()
    {
        var memoryStream = new MemoryStream();
        var excelDocument = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook, true);
        var mainPart = excelDocument.AddWorkbookPart();
        mainPart.Workbook = new Workbook();
        mainPart.Workbook.AppendChild(new Sheets());

        MainContainer = excelDocument;

        SwitchToNewSheet("Catalog");
    }

    /// <summary>
    /// Converts the main container to result.
    /// </summary>
    /// <returns>Template render result.</returns>
    protected override TemplateRenderResult ConvertMainContainerToResult()
    {
        ApplyColumnsWidths();

        MainContainer.Save();

        var memoryStream = new MemoryStream();
        MainContainer.Clone(memoryStream);
        memoryStream.Position = 0;
        MainContainer.Dispose();

        return new TemplateRenderResult(
            memoryStream,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"Product catalog ({DateTime.Now:dd.MM.yyyy}).xlsx");
    }

    /// <summary>
    /// Creates table.
    /// </summary>
    /// <returns>Table.</returns>
    protected override Table CreateTable()
    {
        var table = new Table();
        return table;
    }

    /// <summary>
    /// Appends table to result.
    /// </summary>
    /// <param name="table">Table.</param>
    protected override void AppendTable(Table table)
    {
        var rows = table.ChildElements.OfType<Row>().ToList();
        table.RemoveAllChildren();

        foreach (var row in rows)
        {
            _currentSheetData.AppendChild(row);
        }
    }

    /// <summary>
    /// Appends table group name.
    /// </summary>
    /// <param name="name">Group name.</param>
    protected override void AppendTableGroupName(string name)
    {
        SwitchToNewSheet(name);
    }

    /// <summary>
    /// Creates table row.
    /// </summary>
    /// <returns>Table row.</returns>
    protected override Row CreateTableRow()
    {
        var tableRow = ExcelDocumentHelper.CreateTableRow(ref _currentRowIndex, ref _currentColumnIndex);
        return tableRow;
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

        var currentSheetId = _nextSheetId - 1;
        var currentWidths = _sheetColumnWidths[currentSheetId];

        ExcelDocumentHelper.UpdateColumnsLength(text, currentWidths, _currentColumnIndex);

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

    private void SwitchToNewSheet(string sheetName)
    {
        var workbookPart = MainContainer.WorkbookPart;
        var sheets = workbookPart.Workbook.GetFirstChild<Sheets>();

        _currentWorksheetPart = workbookPart.AddNewPart<WorksheetPart>();
        _currentSheetData = new SheetData();
        _currentWorksheetPart.Worksheet = new Worksheet(_currentSheetData);

        var sheet = ExcelDocumentHelper.CreateSheet(
            workbookPart, _currentWorksheetPart, _nextSheetId, sheetName, sheets);

        sheets.Append(sheet);

        _sheetColumnWidths[_nextSheetId] = [];

        _currentRowIndex = 1;
        _nextSheetId++;
    }

    private void ApplyColumnsWidths()
    {
        var workbookPart = MainContainer.WorkbookPart;
        var sheets = workbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>().ToList();

        foreach (var sheet in sheets)
        {
            var worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
            var worksheet = worksheetPart.Worksheet;
            var sheetData = worksheet.GetFirstChild<SheetData>();

            if (!sheetData.HasChildren)
            {
                sheet.Remove();
                continue;
            }

            if (_sheetColumnWidths.TryGetValue(sheet.SheetId, out var columnWidths))
            {
                ExcelDocumentHelper.ApplyColumnsWidth(columnWidths, worksheet, sheetData);
            }
        }
    }
}
