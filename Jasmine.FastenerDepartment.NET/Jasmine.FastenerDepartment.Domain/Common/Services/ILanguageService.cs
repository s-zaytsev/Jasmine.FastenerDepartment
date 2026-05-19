using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Common.Services;

/// <summary>
/// Language service.
/// </summary>
public interface ILanguageService
{
    /// <summary>
    /// Language code.
    /// </summary>
    LanguageCode? LanguageCode { get; }
}
