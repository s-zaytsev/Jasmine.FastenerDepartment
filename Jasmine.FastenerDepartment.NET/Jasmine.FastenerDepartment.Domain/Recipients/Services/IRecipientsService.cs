using Jasmine.FastenerDepartment.Domain.Recipients.Models;

namespace Jasmine.FastenerDepartment.Domain.Recipients.Services;

/// <summary>
/// Recipients service.
/// </summary>
public interface IRecipientsService
{
    /// <summary>
    /// Returns a collection of recipients.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of recipients.</returns>
    Task<IEnumerable<Recipient>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a recipient.
    /// </summary>
    /// <param name="model">Change recipient model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Recipient.</returns>
    Task<Recipient> CreateAsync(ChangeRecipient model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a recipient.
    /// </summary>
    /// <param name="id">Recipient identifier.</param>
    /// <param name="model">Change recipient model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task UpdateAsync(Guid id, ChangeRecipient model, CancellationToken cancellationToken = default);
}
