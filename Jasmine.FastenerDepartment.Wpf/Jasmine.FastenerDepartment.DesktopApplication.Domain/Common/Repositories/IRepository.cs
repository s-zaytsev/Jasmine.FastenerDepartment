using Jasmine.FastenerDepartment.Domain.Common.Interfaces;

namespace Jasmine.FastenerDepartment.Domain.Common.Repositories;

/// <summary>
/// Repository,
/// </summary>
/// <typeparam name="TKey">Identifier type.</typeparam>
/// <typeparam name="TEntity">Entity type.</typeparam>
public interface IRepository<TKey, TEntity> : IEntitiesRepository<TKey, TEntity>
    where TEntity : IAggregateRoot<TKey>
{
    /// <summary>
    /// Adds entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    void Add(TEntity entity);

    /// <summary>
    /// Adds entities.
    /// </summary>
    /// <param name="entities">Entities.</param>
    void AddRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Updates entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    void Update(TEntity entity);

    /// <summary>
    /// Updates entity with time.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <param name="dateTime">Modified date time.</param>
    void UpdateWithTime(TEntity entity, DateTime dateTime);

    /// <summary>
    /// Updates entities.
    /// </summary>
    /// <param name="entities">Entities.</param>
    void UpdateRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Removes entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    void Remove(TEntity entity);

    /// <summary>
    /// Removes entities.
    /// </summary>
    /// <param name="entities">Entities.</param>
    void RemoveRange(IEnumerable<TEntity> entities);
}

