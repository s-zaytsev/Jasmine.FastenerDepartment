using Jasmine.FastenerDepartment.Documents.Export.Models;
using Jasmine.FastenerDepartment.Documents.Export.Services;
using Jasmine.FastenerDepartment.Domain.Products.Repositories;

namespace Jasmine.FastenerDepartment.Application.Services.Documents;

/// <summary>
/// Documents service.
/// </summary>
public class DocumentsService : IDocumentsService
{
    private readonly IExportDocumentsService _exportDocumentService;
    private readonly IProductsRepository _productsRepository;

    /// <summary>
    /// Creates service.
    /// </summary>
    /// <param name="exportDocumentService">Export document service.</param>
    /// <param name="productsRepository">Products repository.</param>
    public DocumentsService(
        IExportDocumentsService exportDocumentService,
        IProductsRepository productsRepository)
    {
        _exportDocumentService = exportDocumentService;
        _productsRepository = productsRepository;
    }

    /// <summary>
    /// Exports document.
    /// </summary>
    /// <param name="request">Export document request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Export document response.</returns>
    public async Task<ExportDocumentResponse> ExportDocumentAsync(
        ExportDocumentRequest request,
        CancellationToken cancellationToken = default)
    {
        var products = await _productsRepository.GetAllAsync(cancellationToken);
        request.Products = products.OrderBy(x => x.Name.Value);

        var response = await _exportDocumentService.ExportDocumentAsync(request, cancellationToken);
        return response;
    }
}
