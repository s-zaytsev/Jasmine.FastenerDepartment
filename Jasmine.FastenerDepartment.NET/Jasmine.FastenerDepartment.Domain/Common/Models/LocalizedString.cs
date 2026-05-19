namespace Jasmine.FastenerDepartment.Domain.Common.Models;

/// <summary>
/// Localized string.
/// </summary>
public class LocalizedString
{
    /// <summary>
    /// English text.
    /// </summary>
    public string En { get; init; }

    /// <summary>
    /// Russian text.
    /// </summary>
    public string Ru { get; init; }

    /// <summary>
    /// Creates localized string.
    /// </summary>
    /// <param name="en">English text.</param>
    /// <param name="ru"></param>
    public LocalizedString(string en, string ru)
    {
        En = en;
        Ru = ru;
    }

    /// <summary>
    /// Returns localized text.
    /// </summary>
    /// <param name="code">Language code.</param>
    /// <returns>Localized text.</returns>
    public string GetText(LanguageCode? code = null)
    {
        return code switch
        {
            LanguageCode.English => En,
            LanguageCode.Russian => Ru,
            _ => En,
        };
    }
}
