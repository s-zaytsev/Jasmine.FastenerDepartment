namespace Jasmine.FastenerDepartment.Application.Models.Synchronization;

/// <summary>
/// Synchronization history entry.
/// </summary>
public class SynchronizationHistoryEntry
{
    /// <summary>
    /// Date.
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    /// List of synchronization history entry items.
    /// </summary>
    public ICollection<SynchronizationHistoryEntryItem> Items { get; set; }
}
