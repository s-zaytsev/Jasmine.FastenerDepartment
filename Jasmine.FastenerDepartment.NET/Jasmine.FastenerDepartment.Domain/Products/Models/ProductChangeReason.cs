using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Products.Models;

/// <summary>
/// Product change reason.
/// </summary>
public class ProductChangeReason : EntityBase<ProductChangeReasonCode>
{
    /// <summary>
    /// Description.
    /// </summary>
    public LocalizedString Description { get; init; }

    private ProductChangeReason() { }

    /// <summary>
    /// Creates the product change reason.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="description">Description.</param>
    public ProductChangeReason(
        ProductChangeReasonCode id,
        LocalizedString description)
    {
        Id = id;
        Description = description;
    }
}
