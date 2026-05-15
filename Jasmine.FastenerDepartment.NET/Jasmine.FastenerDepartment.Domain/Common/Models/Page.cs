namespace Jasmine.FastenerDepartment.Domain.Common.Models;

/// <summary>
/// Page.
/// </summary>
/// <typeparam name="T">Entity type.</typeparam>
public class Page<T>
{
    /// <summary>
    /// Page number.
    /// </summary>
    public int PageNo { get; set; }

    /// <summary>
    /// Page size.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total count.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Total pages.
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Collection of items.
    /// </summary>
    public ICollection<T> Items { get; set; }
}
