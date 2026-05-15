using Jasmine.FastenerDepartment.Domain.Orders.Models;

namespace Jasmine.FastenerDepartment.Domain.Products.Models;

/// <summary>
/// Create order.
/// </summary>
public class CreateOrderModel
{
    /// <summary>
    /// Supplier identifier.
    /// </summary>
    public Guid? SupplierId { get; set; }

    /// <summary>
    /// List of products.
    /// </summary>
    public ICollection<OrderProduct> Products { get; set; }
}
