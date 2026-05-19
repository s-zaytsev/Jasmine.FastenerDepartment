using Jasmine.FastenerDepartment.Domain.Common.Services;
using Jasmine.FastenerDepartment.WebApi.Services.Language;

namespace Jasmine.FastenerDepartment.WebApi;

/// <summary>
/// Service collection extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds web API services.
    /// </summary>
    /// <param name="services">Service collection.</param>
    public static void AddWebServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ILanguageService, HttpContextLanguageService>();
        services.AddScoped<WebApiMapper>();
    }
}
