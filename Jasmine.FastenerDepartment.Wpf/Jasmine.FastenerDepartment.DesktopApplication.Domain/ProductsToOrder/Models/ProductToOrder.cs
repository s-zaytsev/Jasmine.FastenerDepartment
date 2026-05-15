using Jasmine.FastenerDepartment.Domain.Products.Models;

namespace Jasmine.FastenerDepartment.Domain.ProductsToOrder.Models;

/// <summary>
/// Product to order.
/// </summary>
public class ProductToOrder
{
    /// <summary>
    /// Product.
    /// </summary>
    public Product Product { get; set; }

    /// <summary>
    /// Suppliers product numbers.
    /// </summary>
    public ICollection<SupplierNumber> SupplierNumbers { get; set; }
}
