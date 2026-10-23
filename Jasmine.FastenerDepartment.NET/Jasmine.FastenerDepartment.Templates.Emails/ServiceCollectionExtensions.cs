using Jasmine.FastenerDepartment.Templates.Emails.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Jasmine.FastenerDepartment.Messaging;

/// <summary>
/// Service collection extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Messaging services.
    /// </summary>
    /// <param name="services">Service collection.</param>
    public static void AddEmailTemplatesServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailTemplatesFactory, EmailTemplatesFactory>();
    }
}
