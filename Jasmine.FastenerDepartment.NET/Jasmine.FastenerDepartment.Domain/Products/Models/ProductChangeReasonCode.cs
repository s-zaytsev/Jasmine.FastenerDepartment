namespace Jasmine.FastenerDepartment.Domain.Products.Models;

/// <summary>
/// Product change reason code.
/// </summary>
public enum ProductChangeReasonCode
{
    /// <summary>
    /// Product has been created.
    /// </summary>
    Created = 1,

    /// <summary>
    /// Product number has been changed.
    /// </summary>
    ChangedNumber = 2,

    /// <summary>
    /// Product name has been changed.
    /// </summary>
    ChangedName = 3,

    /// <summary>
    /// Product price has been changed.
    /// </summary>
    ChangedPrice = 4,

    /// <summary>
    /// Product price tag code has been changed.
    /// </summary>
    ChangedPriceTagCode = 5,

    /// <summary>
    /// Product measurement unit code has been changed.
    /// </summary>
    ChangedMeasurementUnitCode = 6,

    /// <summary>
    /// Product order status has been changed.
    /// </summary>
    ChangedOrderStatus = 7,

    /// <summary>
    /// Product print status has been changed.
    /// </summary>
    ChangedPrintStatus = 8,

    /// <summary>
    /// Product has been deleted.
    /// </summary>
    Deleted = 9,

    /// <summary>
    /// Product has been recovered.
    /// </summary>
    Recovered = 10,

    /// <summary>
    /// Product type has been changed.
    /// </summary>
    ChangedType = 11
}
