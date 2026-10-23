using Jasmine.FastenerDepartment.Domain.Documents.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content;

namespace Jasmine.FastenerDepartment.Domain.Documents.Services;

/// <summary>
/// Documents service.
/// </summary>
public interface IExportDocumentsService
{
    /// <summary>
    /// Returns a collection of exporting templates.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of exporting templates.</returns>
    Task<IEnumerable<Template>> GetTemplatesForExportAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Renders a template.
    /// </summary>
    /// <param name="model">Download export document.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Template render result.</returns>
    Task<TemplateRenderResult> DownloadAsync(DownloadExportDocument model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a document.
    /// </summary>
    /// <param name="model">Send document model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task SendAsync(SendExportDocument model, CancellationToken cancellationToken = default);
}
