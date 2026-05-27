using Microsoft.EntityFrameworkCore;
using Jasmine.FastenerDepartment.EF;

namespace Jasmine.FastenerDepartment.WebApi.Configuration;

/// <summary>
/// Database configuration provider.
/// </summary>
public class DatabaseConfigurationProvider : ConfigurationProvider
{
    private readonly Action<DbContextOptionsBuilder<ApplicationDbContext>> _options;

    /// <summary>
    /// Creates provider.
    /// </summary>
    /// <param name="options">Options.</param>
    public DatabaseConfigurationProvider(
        Action<DbContextOptionsBuilder<ApplicationDbContext>> options)
    {
        _options = options;
    }

    /// <summary>
    /// Loads database settings.
    /// </summary>
    public override void Load()
    {
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        _options(builder);

        using var context = new ApplicationDbContext(builder.Options);

        try
        {
            Data = context.Settings.ToDictionary(x => x.Id, x => x.Value);
        }
        catch (Exception)
        {
            // Could be caused on the first migration
        }
    }
}
