using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;

namespace Jasmine.FastenerDepartment.Domain.Templates.Models.Content.ProductCatalog;

/// <summary>
/// Product catalog template render data.
/// </summary>
public class ProductCatalogTemplateRenderData : TemplateRenderData
{
    /// <summary>
    /// Product catalog template content.
    /// </summary>
    public ProductCatalogTemplateContent Content { get; init; }

    /// <summary>
    /// Collection of products.
    /// </summary>
    public IEnumerable<Product> Products { get; init; }

    /// <summary>
    /// Creates a product catalog template render data.
    /// </summary>
    /// <param name="content">Catalog template content.</param>
    /// <param name="products">Collection of products.</param>
    /// <param name="languageCode">Language code.</param>
    public ProductCatalogTemplateRenderData(
        TemplateContent content,
        IEnumerable<Product> products,
        LanguageCode? languageCode)
        : base(languageCode)
    {
        Content = content as ProductCatalogTemplateContent;
        Products = products;
    }
}
