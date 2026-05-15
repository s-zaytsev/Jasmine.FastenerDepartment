using Jasmine.FastenerDepartment.Documents.Export.Models;

namespace Jasmine.FastenerDepartment.Application.Services.Documents;

/// <summary>
/// Documents service.
/// </summary>
public interface IDocumentsService
{
    /// <summary>
    /// Returns export document response.
    /// </summary>
    /// <param name="request">Export document request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Export document response.</returns>
    Task<ExportDocumentResponse> ExportDocumentAsync(
        ExportDocumentRequest request, CancellationToken cancellationToken = default);
}
