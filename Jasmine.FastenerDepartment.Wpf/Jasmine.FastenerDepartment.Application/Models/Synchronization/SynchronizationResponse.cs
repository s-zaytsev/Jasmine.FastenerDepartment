namespace Jasmine.FastenerDepartment.Application.Models.Synchronization;

/// <summary>
/// Synchronization response.
/// </summary>
public class SynchronizationResponse
{
    /// <summary>
    /// List of new synchronization products.
    /// </summary>
    public ICollection<SynchronizationProduct> NewProducts { get; set; }

    /// <summary>
    /// List of modified synchronization products.
    /// </summary>
    public ICollection<SynchronizationProduct> ModifiedProducts { get; set; }

    /// <summary>
    /// List of synchronization history entries.
    /// </summary>
    public ICollection<SynchronizationHistoryEntry> HistoryEntries { get; set; }
}
