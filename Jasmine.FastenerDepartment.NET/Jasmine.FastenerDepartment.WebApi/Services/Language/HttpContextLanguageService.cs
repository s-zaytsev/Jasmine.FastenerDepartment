using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Common.Services;

namespace Jasmine.FastenerDepartment.WebApi.Services.Language;

internal class HttpContextLanguageService : ILanguageService
{
    private LanguageCode _languageCode;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LanguageCode? LanguageCode => _languageCode;

    public HttpContextLanguageService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        SetLanguageCode();
    }

    public void SetLanguageCode()
    {
        var request = _httpContextAccessor.HttpContext?.Request;
        var acceptLanguage = request?.Headers.AcceptLanguage.FirstOrDefault();

        _languageCode = acceptLanguage switch
        {
            "en" => Domain.Common.Models.LanguageCode.English,
            "ru" => Domain.Common.Models.LanguageCode.Russian,
            _ => Domain.Common.Models.LanguageCode.English,
        };
    }
}
