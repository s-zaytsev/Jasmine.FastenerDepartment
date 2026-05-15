using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;

namespace Jasmine.FastenerDepartment.Domain.Suppliers.Models;

/// <summary>
/// Supplier product.
/// </summary>
public class SupplierProduct : AggregateRootBase<Guid>
{
    /// <summary>
    /// Supplier identifier.
    /// </summary>
    public Guid SupplierId { get; private set; }

    /// <summary>
    /// Product identifier.
    /// </summary>
    public Guid ProductId { get; private set; }
    
    /// <summary>
    /// Number from supplier's database.
    /// </summary>
    public string Number { get; private set; }

    /// <summary>
    /// Supplier.
    /// </summary>
    public Supplier Supplier { get; private set; }
    
    /// <summary>
    /// Product.
    /// </summary>
    public Product Product { get; private set; }

    private SupplierProduct() { }

    /// <summary>
    /// Creates the supplier product.
    /// </summary>
    /// <param name="supplierId">Supplier identifier.</param>
    /// <param name="productId">Product identifier.</param>
    /// <param name="number">Number from supplier database.</param>
    public SupplierProduct(
        Guid supplierId,
        Guid productId,
        string number = default)
    {
        SupplierId = supplierId;
        ProductId = productId;
        Number = number;
    }

    /// <summary>
    /// Number.
    /// </summary>
    /// <param name="number">Number.</param>
    public void ChangeNumber(string number)
    {
        Number = number;
    }
}
