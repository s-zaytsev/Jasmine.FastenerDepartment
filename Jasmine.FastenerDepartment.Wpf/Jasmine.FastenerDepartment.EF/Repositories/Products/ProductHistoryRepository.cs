using Jasmine.FastenerDepartment.Domain.HistoryEntries.Models;
using Jasmine.FastenerDepartment.Domain.HistoryEntries.Repositories;
using Jasmine.FastenerDepartment.EF.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Jasmine.FastenerDepartment.EF.Repositories.Products;

/// <summary>
/// Product history repository.
/// </summary>
internal class ProductHistoryRepository : EntitiesRepositoryBase<Guid, ProductHistoryEntry>, IProductHistoryRepository
{
    /// <summary>
    /// Creates repository.
    /// </summary>
    /// <param name="context">Context.</param>
    public ProductHistoryRepository(ApplicationDbContext context)
        : base(context)
    { }

    /// <summary>
    /// Returns list of product history entries.
    /// </summary>
    /// <param name="dateFrom">Date from.</param>
    /// <param name="dateTo">Date to.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of product history entries.</returns>
    public async Task<IEnumerable<ProductHistoryEntry>> GetByDatesAsync(
        DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken = default)
    {
        return await GetQuery()
            .Where(x => x.CreatedDate >= dateFrom && x.CreatedDate <= dateTo)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Returns list of daily history.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of daily history.</returns>
    public async Task<IEnumerable<DailyHistory>> GetDailyHistoryAsync(CancellationToken cancellationToken = default)
    {
        return await GetQuery()
            .Where(x => x.CreatedDate.Date >= DateTime.UtcNow.AddMonths(-1).Date)
            .GroupBy(x => x.CreatedDate.Date)
            .Select(x => new DailyHistory
            {
                Date = x.Key,
                HistoryEntries = x.OrderByDescending(x => x.CreatedDate).ToList()
            })
            .ToListAsync(cancellationToken);
    }

    protected override IQueryable<ProductHistoryEntry> GetQuery()
    {
        return base.GetQuery()
            .Include(x => x.Product);
    }
}
