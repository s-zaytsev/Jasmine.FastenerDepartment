using Jasmine.FastenerDepartment.Domain.HistoryEntries.Repositories;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Repositories;
using Jasmine.FastenerDepartment.Domain.Orders.Repositories;
using Jasmine.FastenerDepartment.Domain.Products.Repositories;
using Jasmine.FastenerDepartment.Domain.ProductsToOrder.Repositories;
using Jasmine.FastenerDepartment.Domain.ProductTypes.Repositories;
using Jasmine.FastenerDepartment.Domain.Settings.Repositories;
using Jasmine.FastenerDepartment.Domain.Suppliers.Repositories;
using Jasmine.FastenerDepartment.EF.Repositories.MeasurementUnits;
using Jasmine.FastenerDepartment.EF.Repositories.Orders;
using Jasmine.FastenerDepartment.EF.Repositories.Products;
using Jasmine.FastenerDepartment.EF.Repositories.ProductsToOrder;
using Jasmine.FastenerDepartment.EF.Repositories.ProductTypes;
using Jasmine.FastenerDepartment.EF.Repositories.SettingsEntries;
using Jasmine.FastenerDepartment.EF.Repositories.Suppliers;
using Jasmine.FastenerDepartment.EF.Repositories.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Jasmine.FastenerDepartment.EF;

/// <summary>
/// Service collection extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Entity Framework services.
    /// </summary>
    /// <param name="services">Service collection.</param>
    public static void AddEFServices(this IServiceCollection services)
    {
        services.AddScoped<IProductsRepository, ProductsRepository>();
        services.AddScoped<IProductHistoryRepository, ProductHistoryRepository>();
        services.AddScoped<IMeasurementUnitsRepository, MeasurementUnitsRepository>();
        services.AddScoped<ISuppliersRepository, SuppliersRepository>();
        services.AddScoped<ISupplierProductsRepository, SupplierProductsRepository>();
        services.AddScoped<IOrdersRepository, OrdersRepository>();
        services.AddScoped<IProductTypesRepository, ProductTypesRepository>();
        services.AddScoped<IProductsToOrderRepository, ProductsToOrderRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISettingsEntriesRepository, SettingsEntriesRepository>();
    }
}
