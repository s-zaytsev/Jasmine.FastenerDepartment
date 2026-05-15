using Jasmine.FastenerDepartment.WebApi.Dtos.HistoryEntries;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Synchronization;

/// <summary>
/// Synchronization history entry item.
/// </summary>
/// <param name="ProductId">Product identifier.</param>
/// <param name="ProductHistoryEntries">Product history entries.</param>
public record SynchronizationHistoryEntryItemDto(
    Guid ProductId,
    IEnumerable<ProductHistoryEntryDto> ProductHistoryEntries);
