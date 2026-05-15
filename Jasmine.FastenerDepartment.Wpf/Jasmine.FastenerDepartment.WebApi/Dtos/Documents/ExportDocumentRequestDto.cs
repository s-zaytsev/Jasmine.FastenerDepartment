using Jasmine.FastenerDepartment.Documents.Export.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Documents;

/// <summary>
/// Export document request.
/// </summary>
/// <param name="DocumentType">Document type.</param>
public record ExportDocumentRequestDto(DocumentType DocumentType);
