using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.WebApi.Dtos.Products;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.HistoryEntries;

/// <summary>
/// Product history entry.
/// </summary>
/// <param name="Id">Identifier.</param>
/// <param name="CreatedDate">Created date.</param>
/// <param name="ProductId">Product identifier.</param>
/// <param name="Reason">Reason.</param>
/// <param name="OldValue">Old value.</param>
/// <param name="NewValue">New value.</param>
/// <param name="ProductNumber">Product number.</param>
public record ProductHistoryEntryDto(
    Guid Id,
    DateTime CreatedDate,
    Guid ProductId,
    ProductChangeReasonDto Reason,
    string OldValue,
    string NewValue,
    int ProductNumber);
