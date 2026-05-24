using Jasmine.FastenerDepartment.Templates.Models;
using Jasmine.FastenerDepartment.Templates.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Jasmine.FastenerDepartment.Templates.Factories;

internal class TemplateProvidersFactory : ITemplateProvidersFactory
{
    private readonly IServiceProvider _serviceProvider;

    public TemplateProvidersFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ITemplateProvider GetProvider(TemplateType type)
    {
        return type switch
        {
            TemplateType.Html => _serviceProvider.GetRequiredService<IHtmlTemplateProvider>(),
            _ => throw new NotSupportedException($"Type {type} not supported.")
        };
    }
}
