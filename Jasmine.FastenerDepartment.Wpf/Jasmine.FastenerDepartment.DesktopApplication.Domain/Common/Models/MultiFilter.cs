namespace Jasmine.FastenerDepartment.Domain.Common.Models;

/// <summary>
/// Multi filter.
/// </summary>
/// <typeparam name="TIdentifier">Identifier.</typeparam>
public class MultiFilter<TIdentifier>
{
    /// <summary>
    /// Collection of filters.
    /// </summary>
    public ICollection<Filter<TIdentifier>> Items { get; set; }
}
