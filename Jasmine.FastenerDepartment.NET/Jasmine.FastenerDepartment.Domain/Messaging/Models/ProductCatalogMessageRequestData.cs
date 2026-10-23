using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;

namespace Jasmine.FastenerDepartment.Domain.Messaging.Models;

/// <summary>
/// Product catalog message request data.
/// </summary>
public class ProductCatalogMessageRequestData : MessageRequestData
{
    /// <summary>
    /// Collection of products.
    /// </summary>
    public IEnumerable<Product> Products { get; set; }

    /// <summary>
    /// Creates a request data.
    /// </summary>
    public ProductCatalogMessageRequestData()
    { }

    /// <summary>
    /// Creates a request data.
    /// </summary>
    /// <param name="products">Collections of products.</param>
    /// <param name="languageCode">Language code.</param>
    public ProductCatalogMessageRequestData(
        IEnumerable<Product> products,
        LanguageCode? languageCode)
    {
        Products = products;
        LanguageCode = languageCode;
    }
}
