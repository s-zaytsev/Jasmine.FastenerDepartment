namespace Jasmine.FastenerDepartment.Domain.Common.Models;

/// <summary>
/// Query base.
/// </summary>
/// <typeparam name="T">Sort type.</typeparam>
public abstract class QueryBase<T>
    where T : Enum
{
    /// <summary>
    /// Search text.
    /// </summary>
    public string Search { get; set; }

    /// <summary>
    /// Sort type.
    /// </summary>
    public T SortBy { get; set; }

    /// <summary>
    /// Sort descending.
    /// </summary>
    public bool SortDesc { get; set; }

    /// <summary>
    /// Page number.
    /// </summary>
    public int PageNo { get; set; }

    /// <summary>
    /// Page size.
    /// </summary>
    public int PageSize { get; set; }
}
