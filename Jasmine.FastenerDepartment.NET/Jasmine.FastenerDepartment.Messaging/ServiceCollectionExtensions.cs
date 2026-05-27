using Jasmine.FastenerDepartment.Domain.Settings.Models.Emails;
using Jasmine.FastenerDepartment.Messaging.Factories;
using Jasmine.FastenerDepartment.Messaging.Services;
using Microsoft.Extensions.Configuration;
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
    /// <param name="configuration">Configuration.</param>
    public static void AddMessagingServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMessageFactory, MessageFactory>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IMessageService, EmailService>();

        services.Configure<EmailSettings>(configuration.GetSection("Emails"));
    }
}
