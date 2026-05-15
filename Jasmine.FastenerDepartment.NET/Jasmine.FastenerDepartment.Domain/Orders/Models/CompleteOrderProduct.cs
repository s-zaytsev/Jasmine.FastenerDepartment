using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Products.Models;

/// <summary>
/// Complete order product model.
/// </summary>
public class CompleteOrderProduct
{
    /// <summary>
    /// Order product identifier.
    /// </summary>
    public Guid? OrderProductId { get; set; }
    
    /// <summary>
    /// Product identifier.
    /// </summary>
    public Guid? ProductId { get; set; }

    /// <summary>
    /// Product name.
    /// </summary>
    public string ProductName { get; set; }

    /// <summary>
    /// Product type identifier.
    /// </summary>
    public Guid? ProductTypeId { get; set; }

    /// <summary>
    /// Price.
    /// </summary>
    public Price Price { get; set; }

    /// <summary>
    /// Supplier product identifier.
    /// </summary>
    public string SupplierProductNumber { get; set; }

    /// <summary>
    /// Is fulfilled.
    /// </summary>
    public bool IsFulfilled { get; set; }

    /// <summary>
    /// Should products from order be added to print list?
    /// </summary>
    public bool AddToPrint { get; set; }

    /// <summary>
    /// Should the order status be removed from the products in the order?
    /// </summary>
    public bool RemoveOrderStatus { get; set; }

    /// <summary>
    /// Fulfilled quantity.
    /// </summary>
    public Quantity Fulfilled { get; set; }
}
