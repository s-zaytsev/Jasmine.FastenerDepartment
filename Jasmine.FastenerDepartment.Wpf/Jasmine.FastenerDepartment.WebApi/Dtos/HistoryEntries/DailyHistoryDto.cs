namespace Jasmine.FastenerDepartment.WebApi.Dtos.HistoryEntries;

/// <summary>
/// Daily history.
/// </summary>
/// <param name="Date">Date.</param>
/// <param name="HistoryEntries">History entries.</param>
public record DailyHistoryDto(
    DateTime Date,
    IEnumerable<ProductHistoryEntryDto> HistoryEntries);
