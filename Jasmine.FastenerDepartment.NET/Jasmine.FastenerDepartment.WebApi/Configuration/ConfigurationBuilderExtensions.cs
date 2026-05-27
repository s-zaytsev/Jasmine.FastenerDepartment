using Microsoft.EntityFrameworkCore;
using Jasmine.FastenerDepartment.EF;

namespace Jasmine.FastenerDepartment.WebApi.Configuration;

/// <summary>
/// Configuration builder extensions.
/// </summary>
public static class ConfigurationBuilderExtensions
{
    /// <summary>
    /// Adds database configuration.
    /// </summary>
    /// <param name="builder">Configuration builder.</param>
    /// <param name="setup">Options.</param>
    public static IConfigurationBuilder AddDatabaseConfiguration(
        this IConfigurationBuilder builder, Action<DbContextOptionsBuilder<ApplicationDbContext>> setup)
    {
        return builder.Add(new DatabaseConfigurationSource(setup));
    }
}
