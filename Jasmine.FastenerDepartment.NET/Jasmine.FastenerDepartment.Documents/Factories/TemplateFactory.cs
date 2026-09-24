using Jasmine.FastenerDepartment.Documents.Providers;
using Jasmine.FastenerDepartment.Domain.Templates.Factories;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content;
using Microsoft.Extensions.DependencyInjection;

namespace Jasmine.FastenerDepartment.Documents.Factories;

internal class TemplateFactory : ITemplateFactory
{
    private readonly IServiceProvider _serviceProvider;

    public TemplateFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TemplateRenderResult Render(
        TemplateFormatCode templateFormat,
        TemplateTypeCode templateType,
        TemplateRenderData data)
    {
        return templateFormat switch
        {
            TemplateFormatCode.Html =>
                _serviceProvider.GetRequiredService<HtmlTemplateProvider>().Render(templateType, data),
            TemplateFormatCode.Word =>
                _serviceProvider.GetRequiredService<WordTemplateProvider>().Render(templateType, data),
            _ => throw new NotSupportedException(),
        };
    }
}
