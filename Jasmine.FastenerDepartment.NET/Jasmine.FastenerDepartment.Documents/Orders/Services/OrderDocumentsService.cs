using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Jasmine.FastenerDepartment.Domain.Orders.Models;

namespace Jasmine.FastenerDepartment.Documents.Orders.Services;

internal class OrderDocumentsService : IOrderDocumentsService
{
    public async Task<Stream> GetStreamAsync(Order order)
    {
        var memoryStream = new MemoryStream();

        using (var wordDocument = WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document, true))
        {
            var mainPart = wordDocument.AddMainDocumentPart();
            mainPart.Document = new Document();

            var body = mainPart.Document.AppendChild(new Body());

            var table = CreateTable();

            var headers = CreateTableHeaders(order.StatusCode);
            table.Append(headers);

            foreach (var product in order.Products)
            {
                var row = CreateTableRow(product, order.StatusCode);
                table.Append(row);
            }

            body.Append(table);
        }

        memoryStream.Position = 0;
        return memoryStream;
    }

    private Table CreateTable()
    {
        var table = new Table();
        var tableProperties = CreateTableProperties();
        table.AppendChild(tableProperties);

        return table;
    }

    private TableProperties CreateTableProperties()
    {
        var properties = new TableProperties(
            new TableBorders(
                CreateBorder<TopBorder>(),
                CreateBorder<BottomBorder>(),
                CreateBorder<LeftBorder>(),
                CreateBorder<RightBorder>(),
                CreateBorder<InsideHorizontalBorder>(),
                CreateBorder<InsideVerticalBorder>()
            ),
            new TableWidth()
            {
                Width = "5000",
                Type = TableWidthUnitValues.Pct
            },
            new TableJustification() { Val = TableRowAlignmentValues.Center }
        );

        return properties;
    }

    private T CreateBorder<T>()
        where T : BorderType, new()
    {
        var border = new T()
        {
            Val = new EnumValue<BorderValues>(BorderValues.Single),
            Size = 4
        };

        return border;
    }

    private TableRow CreateTableHeaders(OrderStatusCode code)
    {
        var headers = new List<TableCell>()
        {
            CreateTableHeader("Артикул поставщика"),
            CreateTableHeader("Наименование товара"),
            CreateTableHeader("Заказано")
        };

        if (code == OrderStatusCode.Cancelled)
        {
            headers.Add(CreateTableHeader(" "));
        }

        if (code == OrderStatusCode.Fulfilled)
        {
            headers.Add(CreateTableHeader("Доставлено"));
        }

        var tableRow = new TableRow();

        foreach (var header in headers)
        {
            tableRow.Append(header);
        }

        return tableRow;
    }

    private TableCell CreateTableHeader(string title)
    {
        var headerCell = CreateTableCell(title);
        return headerCell;
    }

    private TableRow CreateTableRow(OrderProduct product, OrderStatusCode code)
    {
        var row = new TableRow();

        var supplierProductNumberCell = CreateTableCell(product.SupplierProductNumber, type: TableWidthUnitValues.Dxa);
        var nameCell = CreateTableCell(product.ProductName.Value, 1300);

        var orderedMeasurementUnit = product.Ordered.MeasurementUnitCode.HasValue ?
            product.Ordered.MeasurementUnitCode.Value.ToString() :
            product.Ordered.SpecialMeasurementUnit;

        var orderedQuantityCell = CreateTableCell($"{product.Ordered.Value} {orderedMeasurementUnit}");

        row.Append(supplierProductNumberCell);
        row.Append(nameCell);
        row.Append(orderedQuantityCell);

        if (code == OrderStatusCode.Cancelled)
        {
            var cancelledCell = CreateTableCell("-");
            row.Append(cancelledCell);
        }

        if (code == OrderStatusCode.Fulfilled)
        {
            var fulfilledMeasurementUnit = product.Ordered.MeasurementUnitCode.HasValue ?
            product.Ordered.MeasurementUnitCode.Value.ToString() :
            product.Ordered.SpecialMeasurementUnit;

            var fulfilledQuantityCell = CreateTableCell($"{product.Fulfilled.Value} {fulfilledMeasurementUnit}");

            row.Append(fulfilledQuantityCell);
        }

        return row;
    }

    private TableCell CreateTableCell(string text, int? width = null, TableWidthUnitValues type = TableWidthUnitValues.Auto)
    {
        var cellProperties = CreateTableCellProperties(width, type);
        var cell = new TableCell(cellProperties);

        var paragraph = CreateParagraph(text);
        cell.Append(paragraph);

        return cell;
    }

    private TableCellProperties CreateTableCellProperties(int? width, TableWidthUnitValues type)
    {
        var properties = new TableCellProperties(
            new TableCellWidth()
            {
                Type = type,
                Width = width?.ToString() ?? "300"
            });

        return properties;
    }

    private Paragraph CreateParagraph(string text)
    {
        var data = CreateParagraphData(text);
        var paragraph = new Paragraph(data);
        var properties = CreateParagraphProperties();

        paragraph.ParagraphProperties = properties;

        return paragraph;
    }

    private Run CreateParagraphData(string text)
    {
        var data = new Run(
            new RunProperties(
                new FontSize()
                {
                    Val = "20"
                }),
            new Text(text));

        return data;
    }

    private ParagraphProperties CreateParagraphProperties()
    {
        var properties = new ParagraphProperties
        {
            Justification = new Justification { Val = JustificationValues.Center }
        };

        return properties;
    }
}
