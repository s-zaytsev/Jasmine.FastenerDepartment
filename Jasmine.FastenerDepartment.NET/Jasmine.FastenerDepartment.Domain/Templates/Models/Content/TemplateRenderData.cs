using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Templates.Models.Content;

/// <summary>
/// Template render data.
/// </summary>
public abstract class TemplateRenderData
{
    /// <summary>
    /// Language code.
    /// </summary>
    public LanguageCode? LanguageCode { get; init; }

    /// <summary>
    /// Creates template render data.
    /// </summary>
    /// <param name="languageCode">Language code.</param>
    protected TemplateRenderData(
        LanguageCode? languageCode)
    {
        LanguageCode = languageCode;
    }
}
