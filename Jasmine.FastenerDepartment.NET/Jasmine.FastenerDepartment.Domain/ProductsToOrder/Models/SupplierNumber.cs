namespace Jasmine.FastenerDepartment.Domain.ProductsToOrder.Models;

/// <summary>
/// Supplier number.
/// </summary>
public class SupplierNumber
{
    /// <summary>
    /// Supplier identifier.
    /// </summary>
    public Guid SupplierId { get; set; }

    /// <summary>
    /// Number.
    /// </summary>
    public string Number { get; set; }
}
