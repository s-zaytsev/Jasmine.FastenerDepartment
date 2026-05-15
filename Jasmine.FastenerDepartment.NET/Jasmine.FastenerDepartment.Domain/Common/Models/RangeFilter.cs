using System.Numerics;

namespace Jasmine.FastenerDepartment.Domain.Common.Models;

/// <summary>
/// Range filter.
/// </summary>
public class RangeFilter<T> where T : INumber<T>
{
    /// <summary>
    /// Value from.
    /// </summary>
    public T From { get; set; }

    /// <summary>
    /// Value to.
    /// </summary>
    public T To { get; set; }

    /// <summary>
    /// Min value.
    /// </summary>
    public T Min { get; set; }

    /// <summary>
    /// Max value.
    /// </summary>
    public T Max { get; set; }
}
