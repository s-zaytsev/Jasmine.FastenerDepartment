using Jasmine.FastenerDepartment.Documents.Factories;
using Jasmine.FastenerDepartment.Documents.Providers;
using Jasmine.FastenerDepartment.Domain.Templates.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Jasmine.FastenerDepartment.Documents;

/// <summary>
/// Service collection extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds document services.
    /// </summary>
    /// <param name="services">Service collection.</param>
    public static void AddDocumentsServices(this IServiceCollection services)
    {
        services.AddScoped<ITemplateFactory, TemplateFactory>();
        services.AddScoped<HtmlTemplateProvider>();
        services.AddScoped<WordTemplateProvider>();
    }
}