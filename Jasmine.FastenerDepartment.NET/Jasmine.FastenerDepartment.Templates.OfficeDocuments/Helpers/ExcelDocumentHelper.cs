using DocumentFormat.OpenXml.Office2016.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Jasmine.FastenerDepartment.Templates.OfficeDocuments.Helpers;

internal static class ExcelDocumentHelper
{
    internal static Sheet CreateSheet(
        WorkbookPart workbookPart,
        WorksheetPart worksheetPart,
        uint id,
        string name,
        Sheets sheets)
    {
        var safeName = GetSafeSheetName(name, sheets);

        var sheet = new Sheet()
        {
            Id = workbookPart.GetIdOfPart(worksheetPart),
            SheetId = id,
            Name = name
        };

        return sheet;
    }

    internal static Table CreateTable()
    {
        var table = new Table();
        return table;
    }

    internal static Row CreateTableRow(ref uint rowIndex, ref int columnIndex)
    {
        var tableRow = new Row() 
        {
            RowIndex = rowIndex
        };

        columnIndex = 0;
        rowIndex++;

        return tableRow;
    }

    internal static Cell CreateTableRowCell(string text, uint currentRowIndex, ref int currentColumnIndex)
    {
        var columnName = GetColumnName(currentColumnIndex);
        var targetRowIndex = currentRowIndex - 1;

        var cell = new Cell()
        {
            CellReference = $"{columnName}{targetRowIndex}",
            DataType = CellValues.String,
            CellValue = new CellValue(text ?? string.Empty)
        };

        currentColumnIndex++;
        return cell;
    }

    internal static string GetColumnName(int columnIndex)
    {
        string columnName = "";
        while (columnIndex >= 0)
        {
            columnName = (char)('A' + (columnIndex % 26)) + columnName;
            columnIndex = (columnIndex / 26) - 1;
        }
        return columnName;
    }

    internal static void UpdateColumnsLength(string text, Dictionary<int, int> columnsLength, int columnIndex)
    {
        var cellTextLength = text?.Length ?? 0;
        var index = columnIndex - 1;

        if (!columnsLength.TryGetValue(index, out int value))
            columnsLength[index] = cellTextLength;
        else if (cellTextLength > value)
            columnsLength[index] = cellTextLength;
    }

    internal static string GetSafeSheetName(string name, Sheets sheets)
    {
        if (string.IsNullOrEmpty(name)) return "Sheet";

        var invalidChars = new[] { '\\', '/', '?', '*', ':', '[', ']' };
        foreach (var ch in invalidChars)
        {
            name = name.Replace(ch, ' ');
        }

        name = name.Trim();

        var counter = 1;

        var finalName = name;

        while (sheets.Elements<Sheet>().Any(s => s.Name == finalName))
        {
            var suffix = $" ({counter})";
            finalName = name.Length + suffix.Length > 31
                ? name.Substring(0, 31 - suffix.Length) + suffix
                : name + suffix;
            counter++;
        }

        return name.Length > 31 ? name.Substring(0, 31) : name;
    }

    internal static void ApplyColumnsWidth(
        Dictionary<int, int> columnMaxLengths, Worksheet worksheet, SheetData sheetData)
    {
        if (columnMaxLengths.Count > 0)
        {
            var columns = new Columns();

            foreach (var pair in columnMaxLengths)
            {
                var colIndex = pair.Key;
                var maxLength = pair.Value;

                var calculatedWidth = Math.Max(maxLength + 5, 12.0);

                var excelColIndex = (uint)(colIndex + 1);

                var column = new Column()
                {
                    Min = excelColIndex,
                    Max = excelColIndex,
                    Width = calculatedWidth,
                    CustomWidth = true
                };

                columns.AppendChild(column);
            }

            worksheet.InsertBefore(columns, sheetData);
        }
    }
}
