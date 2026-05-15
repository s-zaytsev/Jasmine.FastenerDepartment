using Jasmine.FastenerDepartment.Domain.Common.Interfaces;
using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Common.Repositories;

/// <summary>
/// Entities repository.
/// </summary>
/// <typeparam name="TKey">Identifier type.</typeparam>
/// <typeparam name="TEntity">Entity type.</typeparam>
public interface IEntitiesRepository<TKey, TEntity>
    where TEntity : IEntity<TKey>
{
    /// <summary>
    /// Returns list of entities.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of entities.</returns>
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns an entity by identifier.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Entity.</returns>
    Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns page of entities.
    /// </summary>
    /// <typeparam name="T">Query type.</typeparam>
    /// <typeparam name="K">Sort type.</typeparam>
    /// <param name="query">Query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Page of entities.</returns>
    Task<Page<TEntity>> GetPageAsync<T, K>(T query, CancellationToken cancellationToken = default)
        where T : QueryBase<K>
        where K : Enum;
}
