using Microsoft.EntityFrameworkCore;
using Jasmine.FastenerDepartment.EF;

namespace Jasmine.FastenerDepartment.WebApi.Configuration;

/// <summary>
/// Database configuration source.
/// </summary>
public class DatabaseConfigurationSource : IConfigurationSource
{
    private readonly Action<DbContextOptionsBuilder<ApplicationDbContext>> _options;

    /// <summary>
    /// Creates source.
    /// </summary>
    /// <param name="options">Options.</param>
    public DatabaseConfigurationSource(
        Action<DbContextOptionsBuilder<ApplicationDbContext>> options)
    {
        _options = options;
    }

    /// <summary>
    /// Builds a configuration provider.
    /// </summary>
    /// <param name="builder">Configuration builder.</param>
    /// <returns>Configuration provider.</returns>
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new DatabaseConfigurationProvider(_options);
    }
}
