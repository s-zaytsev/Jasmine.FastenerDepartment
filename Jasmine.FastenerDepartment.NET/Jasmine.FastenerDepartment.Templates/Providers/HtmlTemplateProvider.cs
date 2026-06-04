using Jasmine.FastenerDepartment.Domain.Common.Services;
using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Jasmine.FastenerDepartment.Templates.Builders.Html;

namespace Jasmine.FastenerDepartment.Templates.Providers;

internal class HtmlTemplateProvider : TemplateProviderBase, IHtmlTemplateProvider
{
    public HtmlTemplateProvider(ILanguageService languageService)
        : base(languageService)
    { }

    public string GetOrderRequestTemplate(Order order, bool hasAttachments)
    {
        var template = new HtmlOrderRequestTemplateBuilder(order, hasAttachments)
            .SetLanguage(LanguageService.LanguageCode)
            .AddBody()
            .Build();

        return template;
    }
}
