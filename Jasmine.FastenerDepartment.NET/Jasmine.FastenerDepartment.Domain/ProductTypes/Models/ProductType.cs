using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.ProductTypes.Models;

/// <summary>
/// Product type.
/// </summary>
public class ProductType : AggregateRootBase<Guid>
{
    /// <summary>
    /// Product type name.
    /// </summary>
    public Name Name { get; private set; }

    private ProductType()
    { }

    /// <summary>
    /// Creates the product type.
    /// </summary>
    /// <param name="name">Name.</param>
    public ProductType(string name)
    {
        Name = new(name);
    }

    /// <summary>
    /// Changes the product type name.
    /// </summary>
    /// <param name="name">Name.</param>
    public void ChangeName(string name)
    {
        Name.Value = name;
    }
}
