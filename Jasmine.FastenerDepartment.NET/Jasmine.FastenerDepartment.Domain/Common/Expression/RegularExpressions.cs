using System.Text.RegularExpressions;

namespace Jasmine.FastenerDepartment.Domain.Common.Expression;

/// <summary>
/// Regular expressions.
/// </summary>
public static class RegularExpressions
{
    private static readonly string HardwareMultiSize = @"(\s+[А-ЯA-Z]+\d+)?(\s+\d+[,./-]?\d*[A-Za-zА-Яа-я]*)(\s*[хХxX]\s*\d+[,./-]?\d*[A-Za-zА-Я-а-я]*)+";
    private static readonly string HardwareSingleSize = @"(\s+\d+[,./-]?\d*[A-Za-zА-Яа-я]+\s?)";

    private static readonly string Price = @"[0-9]+,?.?[0-9]?";
    private static readonly string MultiplySpaces = @"(\s)\1+";

    private static readonly Regex HardwareMultiSizeRegex = new(HardwareMultiSize);
    private static readonly Regex HardwareSingleSizeRegex = new(HardwareSingleSize);
    private static readonly Regex PriceRegex = new(Price);
    private static readonly Regex MultiplySpacesRegex = new(MultiplySpaces);

    /// <summary>
    /// Checks the product name for a hardware size.
    /// </summary>
    /// <param name="productName">Product name.</param>
    /// <returns>Does product name have hardware size.</returns>
    public static bool ContainsHardwareSize(string productName)
    {
        if (!HardwareMultiSizeRegex.IsMatch(productName))
        {
            return HardwareSingleSizeRegex.IsMatch(productName);
        }
        return HardwareMultiSizeRegex.IsMatch(productName);
    }

    /// <summary>
    /// Returns the hardware size.
    /// </summary>
    /// <param name="productName">Product name.</param>
    /// <returns>Hardware size.</returns>
    public static string GetHardwareSize(string productName)
    {
        var hardwareSize = HardwareMultiSizeRegex.Match(productName).Value;
        if (string.IsNullOrWhiteSpace(hardwareSize))
        {
            hardwareSize = HardwareSingleSizeRegex.Match(productName).Value;
        }
        return hardwareSize;
    }

    /// <summary>
    /// Returns the price from the text.
    /// </summary>
    /// <param name="price">Price text.</param>
    /// <returns>Price.</returns>
    public static decimal GetPrice(string price)
    {
        var cleanPrice = PriceRegex.Match(price).Value.Replace('.', ',').Replace('/', ',');

        if (string.IsNullOrWhiteSpace(cleanPrice))
        {
            return 0;
        }

        return decimal.Parse(cleanPrice);
    }

    /// <summary>
    /// Removes the multiply spaces from the text.
    /// </summary>
    /// <param name="text">Text.</param>
    /// <returns>Normalized text.</returns>
    public static string RemoveMultiplySpaces(string text)
    {
        return MultiplySpacesRegex.Replace(text, " ");
    }
}
