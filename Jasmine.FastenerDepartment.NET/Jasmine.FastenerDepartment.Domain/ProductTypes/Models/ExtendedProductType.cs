using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.ProductTypes.Models;

/// <summary>
/// Extended product type.
/// </summary>
public class ExtendedProductType
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
    /// Count of products.
    /// </summary>
    public int ProductCount { get; set; }
}
