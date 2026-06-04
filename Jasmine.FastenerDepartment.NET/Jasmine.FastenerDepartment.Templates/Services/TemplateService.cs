using Jasmine.FastenerDepartment.Domain.Common.Services;
using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Jasmine.FastenerDepartment.Templates.Factories;
using Jasmine.FastenerDepartment.Templates.Models;

namespace Jasmine.FastenerDepartment.Templates.Services;

internal class TemplateService : ITemplateService
{
    private readonly ITemplateProvidersFactory _templateServiceFactory;

    public TemplateService(
        ITemplateProvidersFactory templateServiceFactory,
        ILanguageService languageService)
    {
        _templateServiceFactory = templateServiceFactory;
    }

    public string GetOrderRequestTemplate(TemplateType type, Order order, bool hasAttachments)
    {
        var provider = _templateServiceFactory.GetProvider(type);
        var template = provider.GetOrderRequestTemplate(order, hasAttachments);
        return template;
    }
}
