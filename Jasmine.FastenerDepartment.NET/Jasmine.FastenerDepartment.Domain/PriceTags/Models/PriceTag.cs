using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.PriceTags.Models;

/// <summary>
/// Price tag. 
/// </summary>
public class PriceTag : EntityBase<PriceTagCode>
{
    /// <summary>
    /// Name.
    /// </summary>
    public LocalizedString Name { get; private set; }

    private PriceTag() { }

    /// <summary>
    /// Creates the price tag.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="name">Name.</param>
    public PriceTag(
        PriceTagCode id,
        LocalizedString name)
    {
        Id = id;
        Name = name;
    }
}
