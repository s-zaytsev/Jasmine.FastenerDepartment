namespace Jasmine.FastenerDepartment.Domain.Templates.Models.Content.ProductCatalog;

/// <summary>
/// Product catalog render request.
/// </summary>
public class ProductCatalogRenderRequest
{
    /// <summary>
    /// Template identifier.
    /// </summary>
    public Guid TemplateId { get; set; }

    /// <summary>
    /// Format code.
    /// </summary>
    public TemplateFormatCode FormatCode { get; set; }
}
