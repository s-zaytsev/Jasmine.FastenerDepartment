using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.Domain.ProductTypes.Models;
using Jasmine.FastenerDepartment.Domain.Settings.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Jasmine.FastenerDepartment.EF;

/// <summary>
/// Application database context.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Products.
    /// </summary>
    public DbSet<Product> Products { get; set; }

    /// <summary>
    /// Measurement units.
    /// </summary>
    public DbSet<MeasurementUnit> MeasurementUnits { get; set; }

    /// <summary>
    /// Settings.
    /// </summary>
    public DbSet<SettingsEntry> Settings { get; set; }

    /// <summary>
    /// Suppliers.
    /// </summary>
    public DbSet<Supplier> Suppliers { get; set; }

    /// <summary>
    /// Supplier products.
    /// </summary>
    public DbSet<SupplierProduct> SupplierProducts { get; set; }

    /// <summary>
    /// Orders.
    /// </summary>
    public DbSet<Order> Orders { get; set; }

    /// <summary>
    /// Product types.
    /// </summary>
    public DbSet<ProductType> ProductTypes { get; set; }

    /// <summary>
    /// Creates context.
    /// </summary>
    public ApplicationDbContext()
    { }

    /// <summary>
    /// Creates context.
    /// </summary>
    /// <param name="options">Options.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
    { }

    /// <summary>
    /// On configuring.
    /// </summary>
    /// <param name="options">Options.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(connectionString);
        }
    }

    /// <summary>
    /// On model creating.
    /// </summary>
    /// <param name="modelBuilder">Model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetAssembly(typeof(ApplicationDbContext)));
    }
}
