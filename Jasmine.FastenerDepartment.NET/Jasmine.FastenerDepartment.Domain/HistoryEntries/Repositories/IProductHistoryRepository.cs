using Jasmine.FastenerDepartment.Domain.Common.Repositories;
using Jasmine.FastenerDepartment.Domain.HistoryEntries.Models;

namespace Jasmine.FastenerDepartment.Domain.HistoryEntries.Repositories;

/// <summary>
/// Product history repository.
/// </summary>
public interface IProductHistoryRepository : IEntitiesRepository<Guid, ProductHistoryEntry>
{
    /// <summary>
    /// Returns list of product history entries.
    /// </summary>
    /// <param name="dateFrom">Date from.</param>
    /// <param name="dateTo">Date to.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of product history entries.</returns>
    Task<IEnumerable<ProductHistoryEntry>> GetByDatesAsync(
        DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns list of daily history.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of daily history.</returns>
    Task<IEnumerable<DailyHistory>> GetDailyHistoryAsync(CancellationToken cancellationToken = default);
}
