using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Suppliers.Models;

/// <summary>
/// Supplier.
/// </summary>
public class Supplier : AggregateRootBase<Guid>
{
    private List<SupplierProduct> _products = [];

    /// <summary>
    /// Name.
    /// </summary>
    public Name Name { get; private set; }

    /// <summary>
    /// Address.
    /// </summary>
    public string Address { get; private set; }

    /// <summary>
    /// List of supplier products.
    /// </summary>
    public IReadOnlyCollection<SupplierProduct> Products => _products.AsReadOnly();

    private Supplier() { }

    /// <summary>
    /// Creates the supplier.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="address">Address.</param>
    public Supplier(
        string name,
        string address)
    {
        Name = new(name);
        Address = address;
    }

    /// <summary>
    /// Changes the supplier name.
    /// </summary>
    /// <param name="name">Name.</param>
    public void ChangeName(string name)
    {
        Name.Value = name;
    }

    /// <summary>
    /// Changes address.
    /// </summary>
    /// <param name="address">Address.</param>
    public void ChangeAddress(string address)
    {
        Address = address;
    }

    /// <summary>
    /// Adds a supplier product by identifier.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    public void AddProductById(Guid productId)
    {
        if (!_products.Any(x => x.ProductId == productId))
        {
            var supplierProduct = new SupplierProduct(Id, productId);

            _products.Add(supplierProduct);
        }
    }

    /// <summary>
    /// Removes the supplier product by identifier.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    public void RemoveProductById(Guid productId)
    {
        var supplierProduct = _products.FirstOrDefault(x => x.ProductId == productId);
        if (supplierProduct != null)
        {
            _products.Remove(supplierProduct);
        }
    }
}
