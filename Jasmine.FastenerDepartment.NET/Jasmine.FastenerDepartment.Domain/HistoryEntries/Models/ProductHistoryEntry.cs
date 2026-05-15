using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;

namespace Jasmine.FastenerDepartment.Domain.HistoryEntries.Models;

/// <summary>
/// Product history entry.
/// </summary>
public class ProductHistoryEntry : EntityBase<Guid>
{
    /// <summary>
    /// Product identifier.
    /// </summary>
    public Guid ProductId { get; init; }

    /// <summary>
    /// Change reason code.
    /// </summary>
    public ProductChangeReasonCode ChangeReasonCode { get; init; }

    /// <summary>
    /// Old value.
    /// </summary>
    public string OldValue { get; init; }

    /// <summary>
    /// New value.
    /// </summary>
    public string NewValue { get; init; }

    /// <summary>
    /// Created date.
    /// </summary>
    public DateTime CreatedDate { get; init; }

    /// <summary>
    /// Product.
    /// </summary>
    public Product Product { get; private set; }

    /// <summary>
    /// Reason.
    /// </summary>
    public ProductChangeReason Reason { get; private set; }

    private ProductHistoryEntry() { }

    /// <summary>
    /// Creates the product history entry.
    /// </summary>
    /// <param name="changeReasonCode">Change reason code.</param>
    /// <param name="oldValue">Old value.</param>
    /// <param name="newValue">New value.</param>
    /// <param name="createdDate">Created date.</param>
    public ProductHistoryEntry(
        ProductChangeReasonCode changeReasonCode,
        string oldValue,
        string newValue,
        DateTime createdDate)
    {
        ChangeReasonCode = changeReasonCode;
        OldValue = oldValue;
        NewValue = newValue;
        CreatedDate = createdDate;
    }
}
