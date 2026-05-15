using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Jasmine.FastenerDepartment.EF.Extensions;

/// <summary>
/// Service provider extensions.
/// </summary>
public static class ServiceProviderExtensions
{
    /// <summary>
    /// Uses database migrations.
    /// </summary>
    /// <param name="serviceProvider">Service provider.</param>
    public static void UseDatabaseMigrations(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
    }
}