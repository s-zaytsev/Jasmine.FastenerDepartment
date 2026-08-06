using Jasmine.FastenerDepartment.Domain.Companies.Models;

namespace Jasmine.FastenerDepartment.Domain.Companies.Services;

/// <summary>
/// Companies service.
/// </summary>
public interface ICompaniesService
{
    /// <summary>
    /// Returns companies list.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Companies list.</returns>
    Task<IEnumerable<Company>> GetCompaniesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a company.
    /// </summary>
    /// <param name="id">Company identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Company.</returns>
    Task<Company> GetCompanyAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a company.
    /// </summary>
    /// <param name="model">Change company model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Company.</returns>
    Task<Company> CreateCompanyAsync(ChangeCompany model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a company.
    /// </summary>
    /// <param name="id">Company identifier.</param>
    /// <param name="model">Change company model.</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns></returns>
    Task UpdateCompanyAsync(Guid id, ChangeCompany model, CancellationToken cancellationToken = default);
}
