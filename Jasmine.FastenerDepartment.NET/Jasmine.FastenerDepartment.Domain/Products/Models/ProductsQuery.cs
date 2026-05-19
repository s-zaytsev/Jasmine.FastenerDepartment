using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;

namespace Jasmine.FastenerDepartment.Domain.Products.Models;

/// <summary>
/// Products query.
/// </summary>
public class ProductsQuery : QueryBase<ProductsQueryParameter>
{
    /// <summary>
    /// Include deleted products.
    /// </summary>
    public bool? IncludeDeleted { get; set; }

    /// <summary>
    /// Price tags.
    /// </summary>
    public HashSet<PriceTagCode> PriceTags { get; set; }

    /// <summary>
    /// Types.
    /// </summary>
    public HashSet<Guid?> Types { get; set; }

    /// <summary>
    /// Suppliers.
    /// </summary>
    public HashSet<Guid?> Suppliers { get; set; }

    /// <summary>
    /// Price from.
    /// </summary>
    public decimal? PriceFrom { get; set; }

    /// <summary>
    /// Price to.
    /// </summary>
    public decimal? PriceTo { get; set; }

    /// <summary>
    /// Only products to print.
    /// </summary>
    public bool OnlyToPrint { get; set; }

    /// <summary>
    /// Only products to order.
    /// </summary>
    public bool OnlyToOrder { get; set; }

    /// <summary>
    /// Language code.
    /// </summary>
    public LanguageCode? LanguageCode { get; set; }
}
