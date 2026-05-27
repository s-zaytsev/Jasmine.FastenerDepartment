using Jasmine.FastenerDepartment.Application.Services.Documents;
using Jasmine.FastenerDepartment.Application.Services.Orders;
using Jasmine.FastenerDepartment.Application.Services.Print;
using Jasmine.FastenerDepartment.Application.Services.Products;
using Jasmine.FastenerDepartment.Application.Services.ProductsToOrder;
using Jasmine.FastenerDepartment.Application.Services.ProductTypes;
using Jasmine.FastenerDepartment.Application.Services.SettingsEntries;
using Jasmine.FastenerDepartment.Application.Services.Suppliers;
using Jasmine.FastenerDepartment.Application.Services.Synchronization;
using Jasmine.FastenerDepartment.Domain.Orders.Services;
using Jasmine.FastenerDepartment.Domain.Products.Services;
using Jasmine.FastenerDepartment.Domain.ProductsToOrder.Services;
using Jasmine.FastenerDepartment.Domain.ProductTypes.Services;
using Jasmine.FastenerDepartment.Domain.Settings.Services;
using Jasmine.FastenerDepartment.Domain.Suppliers.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Jasmine.FastenerDepartment.Application;

/// <summary>
/// Service collection extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds application services.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Configuration.</param>
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging(configure =>
        {
            configure.ClearProviders();
            configure.AddNLog(configuration);
        });

        services.AddLogging();

        services.AddScoped<IProductsService, ProductsService>();
        services.AddScoped<IOrdersService, OrdersService>();
        services.AddScoped<IDocumentsService, DocumentsService>();
        services.AddScoped<ISynchronizationService, SynchronizationService>();
        services.AddScoped<ISuppliersService, SuppliersService>();
        services.AddScoped<ISupplierProductsService, SupplierProductsService>();
        services.AddScoped<IPrintService, PrintService>();
        services.AddScoped<IProductTypesService, ProductTypesService>();
        services.AddScoped<IProductsToOrderService, ProductsToOrderService>();
        services.AddScoped<ISettingsEntriesService, SettingsEntriesService>();
    }
}