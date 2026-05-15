using Jasmine.FastenerDepartment.Domain.Orders.Models;

namespace Jasmine.FastenerDepartment.Domain.Products.Models;

/// <summary>
/// Change order model.
/// </summary>
public class ChangeOrderModel
{
    /// <summary>
    /// List of products.
    /// </summary>
    public ICollection<OrderProduct> Products { get; set; }
}
