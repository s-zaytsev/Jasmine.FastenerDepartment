namespace Jasmine.FastenerDepartment.Domain.Common.Interfaces;

/// <summary>
/// Entity.
/// </summary>
/// <typeparam name="TKey">Identifier type.</typeparam>
public interface IEntity<TKey>
{
    /// <summary>
    /// Identifier.
    /// </summary>
    TKey Id { get; init; }
}