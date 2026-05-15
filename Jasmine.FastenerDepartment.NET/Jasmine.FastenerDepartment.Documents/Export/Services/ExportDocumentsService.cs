using Jasmine.FastenerDepartment.Documents.Export.Models;

namespace Jasmine.FastenerDepartment.Documents.Export.Services;

internal class ExportDocumentsService : IExportDocumentsService
{
    private readonly IDocumentsServiceFactory _factory;

    public ExportDocumentsService(IDocumentsServiceFactory factory)
    {
        _factory = factory;
    }

    public async Task<ExportDocumentResponse> ExportDocumentAsync(ExportDocumentRequest request, CancellationToken cancellationToken)
    {
        var service = _factory.GetService(request.DocumentType);
        var stream = await service.GetStreamAsync(request.Products, cancellationToken);

        return new ExportDocumentResponse
        {
            Name = $"Жасмин. База товаров от {DateTime.Now.Date:dd.MM.yyyy}.docx",
            Stream = stream
        };
    }
}
