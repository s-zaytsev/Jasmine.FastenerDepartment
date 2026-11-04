using Jasmine.FastenerDepartment.Domain.Templates.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Jasmine.FastenerDepartment.Domain;

/// <summary>
/// Service collection extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds domain services.
    /// </summary>
    /// <param name="services">Service collection.</param>
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<ITemplateContentTableColumnsRepository, TemplateContentTableColumnsRepository>();
    }
}
