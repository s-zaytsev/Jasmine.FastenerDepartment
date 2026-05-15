using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Products;

/// <summary>
/// Product filters.
/// </summary>
public class ProductFiltersDto
{
    /// <summary>
    /// Price tags.
    /// </summary>
    public MultiFilter<PriceTagCode> PriceTags { get; set; }

    /// <summary>
    /// Price range.
    /// </summary>
    public RangeFilter<decimal> PriceRange { get; set; }

    /// <summary>
    /// Types.
    /// </summary>
    public MultiFilter<Guid?> Types { get; set; }

    /// <summary>
    /// Suppliers.
    /// </summary>
    public MultiFilter<Guid?> Suppliers { get; set; }

    /// <summary>
    /// Only products to print.
    /// </summary>
    public SingleFilter OnlyToPrint { get; set; }

    /// <summary>
    /// Only products to order.
    /// </summary>
    public SingleFilter OnlyToOrder { get; set; }
}
