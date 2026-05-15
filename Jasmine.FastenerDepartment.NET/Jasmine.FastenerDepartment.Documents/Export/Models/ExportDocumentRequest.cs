using Jasmine.FastenerDepartment.Domain.Products.Models;

namespace Jasmine.FastenerDepartment.Documents.Export.Models;

/// <summary>
/// Export document request.
/// </summary>
public class ExportDocumentRequest
{
    /// <summary>
    /// Document type.
    /// </summary>
    public DocumentType DocumentType { get; set; }

    /// <summary>
    /// List of products.
    /// </summary>
    public IEnumerable<Product> Products { get; set; }
}
