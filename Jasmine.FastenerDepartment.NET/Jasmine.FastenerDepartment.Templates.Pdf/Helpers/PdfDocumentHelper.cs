using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;

namespace Jasmine.FastenerDepartment.Templates.Pdf.Helpers;

internal static class PdfDocumentHelper
{
    internal static Document CreateMainDocument()
    {
        var document = new Document();

        var section = document.AddSection();

        var backgroundFrame = section.Headers.Primary.AddTextFrame();
        backgroundFrame.FillFormat.Color = Color.Parse("#F5F7FA");

        backgroundFrame.RelativeHorizontal = RelativeHorizontal.Page;
        backgroundFrame.RelativeVertical = RelativeVertical.Page;

        backgroundFrame.Width = document.DefaultPageSetup.PageWidth;
        backgroundFrame.Height = document.DefaultPageSetup.PageHeight;

        backgroundFrame.Top = Unit.Zero;
        backgroundFrame.Left = Unit.Zero;

        return document;
    }

    internal static Table CreateTable<T>(
        IEnumerable<T> codes, Dictionary<T, int> fixedWidthPercent, int unfixedPercent)
    {
        var table = new Table();

        foreach (var code in codes)
        {
            var percent = fixedWidthPercent.TryGetValue(code, out var fixedWidth)
                ? fixedWidth
                : unfixedPercent;

            table.AddColumn(Unit.FromCentimeter((18 * percent) / 100));
        }

        return table;
    }

    internal static Row CreateTableRow()
    {
        var row = new Row();
        row.Shading.Color = Colors.White;
        row.HeightRule = RowHeightRule.Exactly;
        row.Height = Unit.FromCentimeter(1);

        return row;
    }

    internal static Cell CreateTableCell(string text)
    {
        var cell = new Cell();
        cell.AddParagraph(text ?? "");
        cell.Format.Font.Size = Unit.FromPoint(10);
        cell.VerticalAlignment = VerticalAlignment.Center;

        return cell;
    }

    internal static Table CreateTableGroupName(string name)
    {
        var table = new Table();

        var iconColumn = table.AddColumn();
        var nameColumn = table.AddColumn();

        var row = table.AddRow();

        var iconCell = row[0];
        iconCell.Shading.Color = Color.Parse("#7C3AED");
        iconColumn.Width = Unit.FromMillimeter(1.5);

        var nameCell = row[1];
        nameCell.AddParagraph(name);
        nameCell.Format.Font.Size = Unit.FromPoint(14);
        nameColumn.Width = Unit.FromCentimeter(16);

        return table;
    }

    internal static Paragraph CreateParagraph(string text, int fontSize)
    {
        var paragraph = new Paragraph();
        paragraph.Format.Font.Size = Unit.FromPoint(fontSize);
        paragraph.AddText(text);

        return paragraph;
    }
}
