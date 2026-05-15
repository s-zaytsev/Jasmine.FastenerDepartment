namespace Jasmine.FastenerDepartment.Domain.Common.Extensions;

/// <summary>
/// Enum extensions.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Returns the ordinal of enum.
    /// </summary>
    /// <param name="value">Enum.</param>
    /// <returns>Ordinal.</returns>
    public static int Ordinal(this Enum value)
    {
        var ordinal = Convert.ToInt32(value);
        return ordinal;
    }
}
