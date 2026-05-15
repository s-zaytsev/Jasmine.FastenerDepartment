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
    public string Name { get; init; }

    private PriceTag() { }

    /// <summary>
    /// Creates the price tag.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="name">Name.</param>
    public PriceTag(
        PriceTagCode id,
        string name)
    {
        Id = id;
        Name = name;
    }
}
