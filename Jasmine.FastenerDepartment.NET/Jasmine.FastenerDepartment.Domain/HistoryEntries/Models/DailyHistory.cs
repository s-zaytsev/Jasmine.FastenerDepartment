namespace Jasmine.FastenerDepartment.Domain.HistoryEntries.Models;

/// <summary>
/// Daily history.
/// </summary>
public class DailyHistory
{
    /// <summary>
    /// Date.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// List of history entries.
    /// </summary>
    public IEnumerable<ProductHistoryEntry> HistoryEntries { get; set; }
}
