using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;

namespace Jasmine.FastenerDepartment.Domain.Products.Models;

/// <summary>
/// Change product model.
/// </summary>
public class ChangeProductModel
{
    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Product type identifier.
    /// </summary>
    public Guid? TypeId { get; set; }

    /// <summary>
    /// Is hardware size enabled.
    /// </summary>
    public bool IsHardwareSizeEnabled { get; set; }

    /// <summary>
    /// Is needed to order.
    /// </summary>
    public bool IsNeededToOrder { get; set; }

    /// <summary>
    /// Is needed to print.
    /// </summary>
    public bool IsNeededToPrint { get; set; }

    /// <summary>
    /// Price tag code.
    /// </summary>
    public PriceTagCode PriceTagCode { get; set; }

    /// <summary>
    /// Product measurement unit code.
    /// </summary>
    public MeasurementUnitCode MeasurementUnitCode { get; set; }

    /// <summary>
    /// List of supplier identifiers.
    /// </summary>
    public ICollection<Guid> SupplierIds { get; set; }
}
