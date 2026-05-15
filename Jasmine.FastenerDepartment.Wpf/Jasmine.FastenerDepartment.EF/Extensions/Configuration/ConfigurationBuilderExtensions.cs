using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Jasmine.FastenerDepartment.EF.Extensions.Configuration;

/// <summary>
/// Configuration builder extensions.
/// </summary>
public static class ConfigurationBuilderExtensions
{
    /// <summary>
    /// Adds database configuration.
    /// </summary>
    /// <typeparam name="TContext">Context.</typeparam>
    /// <param name="builder">Configuration builder.</param>
    /// <param name="setup">Setup.</param>
    /// <returns>Configuration builder.</returns>
    public static IConfigurationBuilder AddDatabaseConfiguration<TContext>(
        this IConfigurationBuilder builder, Action<DbContextOptionsBuilder<TContext>> setup)
    where TContext : ApplicationDbContext
    {
        return builder.Add(new DatabaseConfigurationSource<TContext>(setup));
    }
}
