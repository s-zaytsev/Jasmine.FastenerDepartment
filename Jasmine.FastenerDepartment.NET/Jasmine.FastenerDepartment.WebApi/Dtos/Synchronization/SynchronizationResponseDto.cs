namespace Jasmine.FastenerDepartment.WebApi.Dtos.Synchronization;

/// <summary>
/// Synchronization response.
/// </summary>
/// <param name="NewProducts">List of the new products.</param>
/// <param name="ModifiedProducts">List of the modified products.</param>
/// <param name="HistoryEntries">List of history entries.</param>
public record SynchronizationResponseDto(
    IEnumerable<SynchronizationProductDto> NewProducts,
    IEnumerable<SynchronizationProductDto> ModifiedProducts,
    IEnumerable<SynchronizationHistoryEntryDto> HistoryEntries);
