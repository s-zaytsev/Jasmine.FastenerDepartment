using Jasmine.FastenerDepartment.Domain.Common.Interfaces;
using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Common.Repositories;
using Jasmine.FastenerDepartment.EF.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jasmine.FastenerDepartment.EF.Repositories.Common;

/// <summary>
/// Repository base.
/// </summary>
/// <typeparam name="TKey">Identifier type.</typeparam>
/// <typeparam name="TEntity">Entity type.</typeparam>
public abstract class RepositoryBase<TKey, TEntity> : IRepository<TKey, TEntity>
    where TEntity : class, IAggregateRoot<TKey>
{
    /// <summary>
    /// Application database context.
    /// </summary>
    protected ApplicationDbContext Context { get; private set; }

    /// <summary>
    /// Creates repository.
    /// </summary>
    /// <param name="context">Context</param>
    public RepositoryBase(ApplicationDbContext context)
    {
        Context = context;
    }

    /// <summary>
    /// Returns list of entities.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of entities.</returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await GetQuery().ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Returns an entity by identifier.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Entity.</returns>
    public virtual Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
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
    /// Adds entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    public void Add(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        entity.CreatedDate = DateTime.UtcNow;
        entity.ModifiedDate = DateTime.UtcNow;
        Context.Add(entity);
    }

    /// <summary>
    /// Adds entities.
    /// </summary>
    /// <param name="entities">Entities.</param>
    public void AddRange(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        foreach (var entity in entities)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.ModifiedDate = DateTime.UtcNow;
            Context.Add(entity);
        }
    }

    /// <summary>
    /// Updates entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    public void Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        entity.ModifiedDate = DateTime.UtcNow;
        Context.Update(entity);
    }

    /// <summary>
    /// Updates entity with time.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <param name="dateTime">Modified date time.</param>
    public void UpdateWithTime(TEntity entity, DateTime dateTime)
    {
        ArgumentNullException.ThrowIfNull(entity);

        entity.ModifiedDate = dateTime;
        Context.Update(entity);
    }

    /// <summary>
    /// Updates entities.
    /// </summary>
    /// <param name="entities">Entities.</param>
    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        foreach (var entity in entities)
        {
            entity.ModifiedDate = DateTime.UtcNow;
            Context.Update(entity);
        }
    }

    /// <summary>
    /// Removes entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    public void Remove(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        Context.Remove(entity);
    }

    /// <summary>
    /// Removes entities.
    /// </summary>
    /// <param name="entities">Entities.</param>
    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        Context.RemoveRange(entities);
    }

    /// <summary>
    /// Returns the query.
    /// </summary>
    /// <returns>Query.</returns>
    protected virtual IQueryable<TEntity> GetQuery()
    {
        return Context.Set<TEntity>();
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
        return x => x.CreatedDate;
    }
}