using Jasmine.FastenerDepartment.Domain.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Jasmine.FastenerDepartment.EF.Extensions;

/// <summary>
/// Queryable extensions.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Converts to page.
    /// </summary>
    /// <typeparam name="T">Entity type.</typeparam>
    /// <param name="query">Query.</param>
    /// <param name="pageNo">Page number.</param>
    /// <param name="pageSize">Page size.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Page.</returns>
    public static async Task<Page<T>> ToPageAsync<T>(
        this IQueryable<T> query,
        int pageNo,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        pageNo = Math.Max(pageNo, 1);
        pageSize = Math.Max(pageSize, 1);
        int totalCount = await query.CountAsync(cancellationToken);
        double totalPages = (double)totalCount / pageSize;
        List<T> items = await query.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToListAsync(cancellationToken);
        return new Page<T>
        {
            Items = items,
            PageNo = pageNo,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalPages)
        };
    }
}
