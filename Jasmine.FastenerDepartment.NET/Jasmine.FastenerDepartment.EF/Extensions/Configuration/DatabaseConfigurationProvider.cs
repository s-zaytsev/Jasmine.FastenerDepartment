using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Jasmine.FastenerDepartment.EF.Extensions.Configuration;

/// <summary>
/// Database configuration provider.
/// </summary>
/// <typeparam name="TContext">Context.</typeparam>
public class DatabaseConfigurationProvider<TContext> : ConfigurationProvider
    where TContext : ApplicationDbContext
{
    private readonly Action<DbContextOptionsBuilder<TContext>> _options;

    /// <summary>
    /// Creates provider.
    /// </summary>
    /// <param name="options">Options.</param>
    public DatabaseConfigurationProvider(
        Action<DbContextOptionsBuilder<TContext>> options)
    {
        _options = options;
    }

    /// <summary>
    /// Loads settings.
    /// </summary>
    public override void Load()
    {
        var builder = new DbContextOptionsBuilder<TContext>();
        _options(builder);

        using var context = (TContext)Activator.CreateInstance(typeof(TContext), builder.Options);
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
