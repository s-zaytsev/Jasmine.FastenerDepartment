using Jasmine.FastenerDepartment.Domain.HistoryEntries.Models;

namespace Jasmine.FastenerDepartment.Application.Models.Synchronization;

/// <summary>
/// Synchronization history entry item.
/// </summary>
public class SynchronizationHistoryEntryItem
{
    /// <summary>
    /// Product identifier.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// List of product history entries.
    /// </summary>
    public ICollection<ProductHistoryEntry> ProductHistoryEntries { get; set; }
}
