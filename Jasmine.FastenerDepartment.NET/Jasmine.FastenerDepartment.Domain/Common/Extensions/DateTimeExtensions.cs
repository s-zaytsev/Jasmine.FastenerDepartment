namespace Jasmine.FastenerDepartment.Domain.Common.Extensions;

/// <summary>
/// Date time extensions.
/// </summary>
public static class DateTimeExtensions
{

    /// <summary>
    /// Sets UTC kind.
    /// </summary>
    /// <param name="dateTime">Date time.</param>
    /// <returns>Date time with UTC kind.</returns>
    public static DateTime? SetKindUtc(this DateTime? dateTime)
    {
        if (dateTime.HasValue)
        {
            return dateTime.Value.SetKindUtc();
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Sets UTC kind.
    /// </summary>
    /// <param name="dateTime">Date time.</param>
    /// <returns>Date time with UTC kind.</returns>
    public static DateTime SetKindUtc(this DateTime dateTime)
    {
        if (dateTime.Kind == DateTimeKind.Utc) 
        {
            return dateTime;
        }

        return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
    }
}
