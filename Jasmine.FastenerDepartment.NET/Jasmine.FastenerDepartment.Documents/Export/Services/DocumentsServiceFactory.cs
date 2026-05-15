using Jasmine.FastenerDepartment.Documents.Export.Models;

namespace Jasmine.FastenerDepartment.Documents.Export.Services;

/// <summary>
/// Document service factory.
/// </summary>
internal class DocumentsServiceFactory : IDocumentsServiceFactory
{
    private readonly IWordExportDocumentsService _wordExportDocumentsService;

    /// <summary>
    /// Creates factory.
    /// </summary>
    /// <param name="wordExportDocumentsService">Word export document service.</param>
    public DocumentsServiceFactory(IWordExportDocumentsService wordExportDocumentsService)
    {
        _wordExportDocumentsService = wordExportDocumentsService;
    }

    /// <summary>
    /// Returns a service.
    /// </summary>
    /// <param name="type">Document type.</param>
    /// <returns>Export service.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public IDocumentsService GetService(DocumentType type)
    {
        switch (type)
        {
            case DocumentType.Word:
                return _wordExportDocumentsService;

            default:
                throw new NotImplementedException($"Document {type} isn't supported.");
        }
    }
}
