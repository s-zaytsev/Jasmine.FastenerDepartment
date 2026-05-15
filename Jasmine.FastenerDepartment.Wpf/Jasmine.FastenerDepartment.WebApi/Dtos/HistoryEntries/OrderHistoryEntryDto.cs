using Jasmine.FastenerDepartment.Domain.Orders.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.HistoryEntries;

/// <summary>
/// Order history entry.
/// </summary>
/// <param name="Id">Identifier.</param>
/// <param name="CreatedDate">Created date.</param>
/// <param name="StatusCode">Status code.</param>
/// <param name="Comment">Comment.</param>
public record OrderHistoryEntryDto(
    Guid Id,
    DateTime CreatedDate,
    OrderStatusCode StatusCode,
    string Comment);
