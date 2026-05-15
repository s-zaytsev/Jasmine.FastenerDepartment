using Jasmine.FastenerDepartment.Domain.Common.Interfaces;

namespace Jasmine.FastenerDepartment.Domain.Common.Models;

/// <summary>
/// Aggregate root base.
/// </summary>
/// <typeparam name="T">Identifier type.</typeparam>
public abstract class AggregateRootBase<T> : EntityBase<T>, IAggregateRoot<T>
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

