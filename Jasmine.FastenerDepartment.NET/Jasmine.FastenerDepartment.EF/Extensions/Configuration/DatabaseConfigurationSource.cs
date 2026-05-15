using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Jasmine.FastenerDepartment.EF.Extensions.Configuration;

/// <summary>
/// Database configuration source.
/// </summary>
/// <typeparam name="TContext">Context.</typeparam>
public class DatabaseConfigurationSource<TContext> : IConfigurationSource
    where TContext : ApplicationDbContext
{
    private readonly Action<DbContextOptionsBuilder<TContext>> _options;

    /// <summary>
    /// Creates source.
    /// </summary>
    /// <param name="options">Options.</param>
    public DatabaseConfigurationSource(Action<DbContextOptionsBuilder<TContext>> options)
    {
        _options = options;
    }

    /// <summary>
    /// Builds source.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new DatabaseConfigurationProvider<TContext>(_options);
    }
}
