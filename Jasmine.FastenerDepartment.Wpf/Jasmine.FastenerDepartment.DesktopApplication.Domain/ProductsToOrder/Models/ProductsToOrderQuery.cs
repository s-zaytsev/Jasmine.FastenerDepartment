namespace Jasmine.FastenerDepartment.Domain.ProductsToOrder.Models;

/// <summary>
/// Products to order query.
/// </summary>
public class ProductsToOrderQuery
{
    /// <summary>
    /// Search.
    /// </summary>
    public string Search { get; set; }

    /// <summary>
    /// Supplier identifier.
    /// </summary>
    public Guid? SupplierId { get; set; }

    /// <summary>
    /// Select only products with IsNeededToOrder flag.
    /// </summary>
    public bool OnlyToOrder { get; set; }
}
