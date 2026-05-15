namespace Jasmine.FastenerDepartment.Domain.Common.Interfaces;

/// <summary>
/// Aggregate root.
/// </summary>
/// <typeparam name="TKey">Identifier type.</typeparam>
public interface IAggregateRoot<TKey> : IEntity<TKey>
{
    /// <summary>
    /// Created date.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Modified date.
    /// </summary>
    public DateTime ModifiedDate { get; set; }
}
