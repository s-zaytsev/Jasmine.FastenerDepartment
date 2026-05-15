using Jasmine.FastenerDepartment.Domain.Common.Interfaces;
using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Common.Repositories;
using Jasmine.FastenerDepartment.EF.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jasmine.FastenerDepartment.EF.Repositories.Common;

/// <summary>
/// Entities repository base.
/// </summary>
/// <typeparam name="TKey">Identifier type.</typeparam>
/// <typeparam name="TEntity">Entity type.</typeparam>
public abstract class EntitiesRepositoryBase<TKey, TEntity> : IEntitiesRepository<TKey, TEntity>
    where TEntity : class, IEntity<TKey>
{
    /// <summary>
    /// Application database context.
    /// </summary>
    protected ApplicationDbContext Context { get; private set; }

    /// <summary>
    /// Creates repository.
    /// </summary>
    /// <param name="context">Context.</param>
    public EntitiesRepositoryBase(ApplicationDbContext context)
    {
        Context = context;
    }

    /// <summary>
    /// Returns list of entities.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of entities.</returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await GetQuery().ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Returns an entity by identifier.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Entity.</returns>
    public Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken)
    {
        return GetQuery().FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }

    /// <summary>
    /// Returns page of entities.
    /// </summary>
    /// <typeparam name="T">Query type.</typeparam>
    /// <typeparam name="K">Sort type.</typeparam>
    /// <param name="query">Query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Page of entities.</returns>
    public async Task<Page<TEntity>> GetPageAsync<T, K>(T query, CancellationToken cancellationToken = default)
        where T : QueryBase<K>
        where K : Enum
    {
        var dbQuery = GetQuery();
        dbQuery = ApplyFiltering<T, K>(dbQuery, query);
        dbQuery = ApplySorting<T, K>(dbQuery, query);

        var result = await dbQuery.ToPageAsync(query.PageNo, query.PageSize, cancellationToken);
        return result;
    }

    /// <summary>
    /// Applies filtering.
    /// </summary>
    /// <typeparam name="T">Query type.</typeparam>
    /// <typeparam name="K">Sort type.</typeparam>
    /// <param name="dbQuery">Database query.</param>
    /// <param name="query">Query.</param>
    /// <returns>Database query with filters.</returns>
    protected IQueryable<TEntity> ApplyFiltering<T, K>(IQueryable<TEntity> dbQuery, T query)
        where T : QueryBase<K>
        where K : Enum
    {
        var filterings = GetFilterings<T, K>(dbQuery, query);
        if (filterings.Any())
        {
            var filtering = filterings.Aggregate((x, y) => x.And(y));
            dbQuery = dbQuery.Where(filtering);
        }

        return dbQuery;
    }

    /// <summary>
    /// Returns filters.
    /// </summary>
    /// <typeparam name="T">Query type.</typeparam>
    /// <typeparam name="K">Sort type.</typeparam>
    /// <param name="dbQuery">Database query.</param>
    /// <param name="query">Query.</param>
    /// <returns>List of filters.</returns>
    protected virtual IEnumerable<Expression<Func<TEntity, bool>>> GetFilterings<T, K>(
            IQueryable<TEntity> dbQuery, T query)
        where T : QueryBase<K>
        where K : Enum
    {
        var predicates = new List<Expression<Func<TEntity, bool>>>();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            predicates.Add(x => x.Id.ToString().StartsWith(query.Search));
        }

        return predicates;
    }

    /// <summary>
    /// Applies sorting.
    /// </summary>
    /// <typeparam name="T">Query type.</typeparam>
    /// <typeparam name="K">Sort type.</typeparam>
    /// <param name="dbQuery">Database query.</param>
    /// <param name="query">Query.</param>
    /// <returns>Database query with sorting.</returns>
    protected IQueryable<TEntity> ApplySorting<T, K>(IQueryable<TEntity> dbQuery, T query)
        where T : QueryBase<K>
        where K : Enum
    {
        var sorting = GetSorting(query.SortBy);
        dbQuery = !query.SortDesc ? dbQuery.OrderBy(sorting) : dbQuery.OrderByDescending(sorting);

        return dbQuery;
    }

    /// <summary>
    /// Returns sorting.
    /// </summary>
    /// <typeparam name="E">Sort type.</typeparam>
    /// <param name="parameter">Sort type.</param>
    /// <returns>Sorting.</returns>
    protected virtual Expression<Func<TEntity, object>> GetSorting<E>(E parameter) where E : Enum
    {
        return x => x.Id;
    }

    /// <summary>
    /// Returns query.
    /// </summary>
    /// <returns>Query.</returns>
    protected virtual IQueryable<TEntity> GetQuery()
    {
        return Context.Set<TEntity>();
    }
}
