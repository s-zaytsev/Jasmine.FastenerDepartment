using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Jasmine.FastenerDepartment.Domain.Products.Models;

namespace Jasmine.FastenerDepartment.Documents.Export.Services;

internal class WordExportDocumentService : IWordExportDocumentsService
{
    /// <summary>
    /// Returns the stream of document
    /// </summary>
    /// <param name="products">List of products.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Stream of the document.</returns>
    public async Task<Stream> GetStreamAsync(IEnumerable<Product> products, CancellationToken cancellationToken = default)
    {
        var memoryStream = new MemoryStream();

        using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document, true))
        {
            MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
            mainPart.Document = new Document();
            Body body = mainPart.Document.AppendChild(new Body());
            wordDocument.MainDocumentPart.Document.Body.Append(CreateTable(products));
        }

        memoryStream.Position = 0;
        return memoryStream;
    }

    private Table CreateTable(IEnumerable<Product> products)
    {
        Table table = new();

        TableProperties tblProp = new(
            new TableBorders(
                new TopBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 4
                },
                new BottomBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 4
                },
                new LeftBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 4
                },
                new RightBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 4
                },
                new InsideHorizontalBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 4
                },
                new InsideVerticalBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 4
                }
            ),
        new TableWidth()
        {
            Width = "5000",
            Type = TableWidthUnitValues.Pct
        },
        new TableJustification() { Val = TableRowAlignmentValues.Center }
        );

        table.AppendChild(tblProp);

        foreach (var product in products)
        {
            TableRow row = new();

            TableCell productNumberCell = new TableCell(
                new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1300" }));

            var idCellParagraph = new Paragraph(
                new Run(new RunProperties(new FontSize() { Val = "28" }), new Text(product.Number.Value.ToString())));

            idCellParagraph.ParagraphProperties = new ParagraphProperties
            {
                Justification = new Justification { Val = JustificationValues.Center }
            };

            productNumberCell.Append(idCellParagraph);

            var priceTagCell = new TableCell(
                new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "200" }));

            var priceTagCellParagraph = new Paragraph(
                new Run(new RunProperties(new FontSize() { Val = "28" }), new Text(product.PriceTag.Name)));

            priceTagCellParagraph.ParagraphProperties = new ParagraphProperties
            {
                Justification = new Justification { Val = JustificationValues.Center }
            };

            priceTagCell.Append(priceTagCellParagraph);

            var nameCell = new TableCell();
            var nameCellParagraph = new Paragraph(
                new Run(new RunProperties(new FontSize() { Val = "28" }), new Text(product.Name.Value)));

            nameCellParagraph.ParagraphProperties = new ParagraphProperties
            {
                Justification = new Justification { Val = JustificationValues.Center }
            };

            nameCell.Append(nameCellParagraph);

            var priceCell = new TableCell();
            var priceCellParagraph = new Paragraph(
                new Run(new RunProperties(
                    new FontSize() { Val = "28" }),
                    new Text($"{product.Price.Value} руб./{product.MeasurementUnit.ShortName}")));

            priceCellParagraph.ParagraphProperties = new ParagraphProperties
            {
                Justification = new Justification { Val = JustificationValues.Center }
            };

            priceCell.Append(priceCellParagraph);

            row.Append(priceTagCell);
            row.Append(productNumberCell);
            row.Append(nameCell);
            row.Append(priceCell);
            table.Append(row);
        }

        return table;
    }
}
