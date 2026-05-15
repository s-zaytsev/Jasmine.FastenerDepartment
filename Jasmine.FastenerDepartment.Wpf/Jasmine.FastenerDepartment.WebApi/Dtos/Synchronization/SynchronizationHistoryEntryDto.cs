namespace Jasmine.FastenerDepartment.WebApi.Dtos.Synchronization;

/// <summary>
/// Synchronization history entry.
/// </summary>
/// <param name="Date">Date.</param>
/// <param name="HistoryItems">History items.</param>
public record SynchronizationHistoryEntryDto(
    DateTime Date,
    IEnumerable<SynchronizationHistoryEntryItemDto> HistoryItems);
