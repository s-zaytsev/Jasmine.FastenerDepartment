using Jasmine.FastenerDepartment.Domain.Common.Interfaces;

namespace Jasmine.FastenerDepartment.Domain.Common.Models;

/// <summary>
/// Entity base.
/// </summary>
/// <typeparam name="T">Identifier type.</typeparam>
public abstract class EntityBase<T> : IEntity<T>
{
    /// <summary>
    /// Identifier.
    /// </summary>
    public T Id { get; init; }
}
