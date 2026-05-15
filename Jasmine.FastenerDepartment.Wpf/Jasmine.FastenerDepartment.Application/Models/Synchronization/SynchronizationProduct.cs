using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;

namespace Jasmine.FastenerDepartment.Application.Models.Synchronization;

/// <summary>
/// Synchronization product.
/// </summary>
public class SynchronizationProduct
{
    /// <summary>
    /// Identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Created date.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Modified date.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    /// <summary>
    /// Number.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Is deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

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
    /// Measurement unit code.
    /// </summary>
    public MeasurementUnitCode MeasurementUnitCode { get; set; }
}
