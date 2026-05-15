using Jasmine.FastenerDepartment.Application.Models.Synchronization;

namespace Jasmine.FastenerDepartment.Application.Services.Synchronization;

/// <summary>
/// Synchronization service.
/// </summary>
public interface ISynchronizationService
{
    /// <summary>
    /// Synchronizes the products.
    /// </summary>
    /// <param name="request">Synchronization request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Synchronization response.</returns>
    Task<SynchronizationResponse> SynchronizeAsync(SynchronizationRequest request, CancellationToken cancellationToken = default);
}
