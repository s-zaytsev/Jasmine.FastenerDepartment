namespace Jasmine.FastenerDepartment.Domain.Common.Models;

/// <summary>
/// Filter.
/// </summary>
/// <typeparam name="TIdentifier">Identifier type</typeparam>
public class Filter<TIdentifier>
{
    /// <summary>
    /// Identifier.
    /// </summary>
    public TIdentifier Id { get; set; }

    /// <summary>
    /// Title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Count.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Is filter enabled.
    /// </summary>
    public bool IsEnabled { get; set; }
}
