using System.Text.RegularExpressions;

namespace Jasmine.FastenerDepartment.ConsoleApplication;
public static class LogsExpressions
{
    private static readonly Regex _productNumberRegex = new("1\\d{7}");
    private static readonly Regex _productMeasurementUnit = new("/\\D+");
    private static readonly Regex _productPrice = new("\\d+[,.]?\\d*");

    public static bool IsStartOfProductInfo(string line)
    {
        return _productNumberRegex.IsMatch(line);
    }

    public static string ProductNumber(string line)
    {
        return _productNumberRegex.Match(line).Value;
    }

    public static string ProducPrice(string line)
    {
        return _productPrice.Match(line).Value?.Replace(".", ",");
    }

    public static string ProductMeasurementUnit(string line)
    {
        return Regex.Replace(_productMeasurementUnit.Match(line).Value.Trim(), @"[^а-яА-Я]", "");
    }
}
