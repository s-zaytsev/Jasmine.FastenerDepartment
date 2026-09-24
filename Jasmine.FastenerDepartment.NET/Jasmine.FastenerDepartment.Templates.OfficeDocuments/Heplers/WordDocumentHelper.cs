using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Jasmine.FastenerDepartment.Templates.OfficeDocuments.Heplers;

internal static class WordDocumentHelper
{
    internal static Table CreateTable()
    {
        var table = new Table();
        var tableProperties = CreateTableProperties();
        table.AppendChild(tableProperties);
        table.AppendChild(new TableGrid());

        return table;
    }

    internal static TableCell CreateTableCell(
        string text,
        string width = null,
        TableWidthUnitValues? type = null,
        string fontSize = null)
    {
        type ??= TableWidthUnitValues.Auto;
        var cellProperties = CreateTableCellProperties(width, type.Value);
        var cell = new TableCell(cellProperties);

        var paragraph = CreateParagraph(text, fontSize);
        cell.Append(paragraph);

        return cell;
    }

    internal static Paragraph CreateParagraph(string text, string fontSize = null, JustificationValues? textAlign = null)
    {
        var data = CreateParagraphData(text, fontSize);
        var paragraph = new Paragraph(data);
        var properties = CreateParagraphProperties(textAlign);

        paragraph.ParagraphProperties = properties;

        return paragraph;
    }

    private static TableProperties CreateTableProperties()
    {
        var properties = new TableProperties(
            new TableWidth()
            {
                Width = "5000",
                Type = TableWidthUnitValues.Pct
            },
            new TableJustification() { Val = TableRowAlignmentValues.Center },
            new TableBorders(
                CreateBorder<TopBorder>(),
                CreateBorder<LeftBorder>(),
                CreateBorder<BottomBorder>(),
                CreateBorder<RightBorder>(),
                CreateBorder<InsideHorizontalBorder>(),
                CreateBorder<InsideVerticalBorder>()
            ),
            new TableCellMarginDefault(
                new StartMargin { Width = "120", Type = TableWidthUnitValues.Dxa},
                new EndMargin { Width = "120", Type = TableWidthUnitValues.Dxa}
            )
        );

        return properties;
    }

    private static T CreateBorder<T>()
        where T : BorderType, new()
    {
        var border = new T()
        {
            Val = new EnumValue<BorderValues>(BorderValues.Single),
            Size = 4
        };

        return border;
    }

    private static TableCellProperties CreateTableCellProperties(string width, TableWidthUnitValues type)
    {
        var properties = new TableCellProperties(
            new TableCellWidth()
            {
                Type = type,
                Width = width ?? "300"
            },
            new TableCellVerticalAlignment() 
            {
                Val = TableVerticalAlignmentValues.Center
            });

        return properties;
    }

    private static Run CreateParagraphData(string text, string fontSize = null)
    {
        var data = new Run(
            new RunProperties(
                new FontSize()
                {
                    Val = fontSize ?? "26"
                }),
            new Text(text));

        return data;
    }

    private static ParagraphProperties CreateParagraphProperties(JustificationValues? textAlign = null)
    {
        var properties = new ParagraphProperties
        {
            Justification = new Justification { Val = textAlign ?? JustificationValues.Left }
        };

        return properties;
    }
}
