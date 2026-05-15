using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Suppliers.Models;

/// <summary>
/// Extended supplier.
/// </summary>
public class ExtendedSupplier
{
    /// <summary>
    /// Identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    public Name Name { get; set; }

    /// <summary>
    /// Address.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Count of products.
    /// </summary>
    public int ProductCount { get; set; }

    /// <summary>
    /// Count of active orders.
    /// </summary>
    public int ActiveOrderCount { get; set; }
}
