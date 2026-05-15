using Jasmine.FastenerDepartment.Domain.Products.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.HistoryEntries;

/// <summary>
/// Product history entry.
/// </summary>
/// <param name="Id">Identifier.</param>
/// <param name="CreatedDate">Created date.</param>
/// <param name="ProductId">Product identifier.</param>
/// <param name="ChangeReasonCode">Change reason code.</param>
/// <param name="OldValue">Old value.</param>
/// <param name="NewValue">New value.</param>
/// <param name="ProductNumber">Product number.</param>
public record ProductHistoryEntryDto(
    Guid Id,
    DateTime CreatedDate,
    Guid ProductId,
    ProductChangeReasonCode ChangeReasonCode,
    string OldValue,
    string NewValue,
    int ProductNumber);
