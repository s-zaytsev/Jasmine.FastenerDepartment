using Jasmine.FastenerDepartment.Domain.Products.Models;

namespace Jasmine.FastenerDepartment.Domain.Templates.Models.Content.ProductCatalog;

/// <summary>
/// Product catalog render request.
/// </summary>
public class ProductCatalogRenderRequest : RenderRequestBase
{

    /// <summary>
    /// Collection of products.
    /// </summary>
    public IEnumerable<Product> Products { get; init; }

    /// <summary>
    /// Creates a product catalog render request.
    /// </summary>
    /// <param name="templateId">Template identifier.</param>
    /// <param name="formatCode">Format code.</param>
    /// <param name="products">Collection of products.</param>
    public ProductCatalogRenderRequest(
        Guid templateId,
        TemplateFormatCode formatCode,
        IEnumerable<Product> products)
        : base(templateId, formatCode)
    {
        Products = products;
    }

}
