using Jasmine.FastenerDepartment.Domain.Common.Services;

namespace Jasmine.FastenerDepartment.Templates.Providers;

internal abstract class TemplateProviderBase
{
    protected readonly ILanguageService LanguageService;

    protected TemplateProviderBase(ILanguageService languageService)
    {
        LanguageService = languageService;
    }
}
