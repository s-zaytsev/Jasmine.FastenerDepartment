using Jasmine.FastenerDepartment.Domain.Products.Models;

namespace Jasmine.FastenerDepartment.Application.Models.Synchronization;

/// <summary>
/// Synchronization request.
/// </summary>
public class SynchronizationRequest
{
    /// <summary>
    /// Last synchronization UTC time.
    /// </summary>
    public DateTime? LastSynchronizeUtcTime { get; set; }

    /// <summary>
    /// List of synchronization products.
    /// </summary>
    public IEnumerable<SynchronizationProduct> Products { get; set; } = [];
}
