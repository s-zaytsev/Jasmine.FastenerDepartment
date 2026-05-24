using Jasmine.FastenerDepartment.Templates.Factories;
using Jasmine.FastenerDepartment.Templates.Providers;
using Jasmine.FastenerDepartment.Templates.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Jasmine.FastenerDepartment.Templates;

/// <summary>
/// Service collection extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Templates services.
    /// </summary>
    /// <param name="services">Service collection.</param>
    public static void AddTemplatesServices(this IServiceCollection services)
    {
        services.AddScoped<ITemplateService, TemplateService>();
        services.AddScoped<ITemplateProvidersFactory, TemplateProvidersFactory>();
        services.AddScoped<IHtmlTemplateProvider, HtmlTemplateProvider>();
    }
}
