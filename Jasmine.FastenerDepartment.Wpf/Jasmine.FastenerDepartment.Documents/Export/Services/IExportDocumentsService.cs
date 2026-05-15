using Jasmine.FastenerDepartment.Documents.Export.Models;

namespace Jasmine.FastenerDepartment.Documents.Export.Services;

/// <summary>
/// Export documents service.
/// </summary>
public interface IExportDocumentsService
{
    /// <summary>
    /// Returns export document response.
    /// </summary>
    /// <param name="request">Export document request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Export document response.</returns>
    Task<ExportDocumentResponse> ExportDocumentAsync(
        ExportDocumentRequest request,
        CancellationToken cancellationToken = default);
}
