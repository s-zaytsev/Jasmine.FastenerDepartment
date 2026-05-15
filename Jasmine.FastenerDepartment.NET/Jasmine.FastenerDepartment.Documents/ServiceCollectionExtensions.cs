using Jasmine.FastenerDepartment.Documents.Export.Services;
using Jasmine.FastenerDepartment.Documents.Orders.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jasmine.FastenerDepartment.Documents;

/// <summary>
/// Service collection extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds document services.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Configuration.</param>
    public static void AddDocumentsServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IExportDocumentsService, ExportDocumentsService>();
        services.AddScoped<IDocumentsServiceFactory, DocumentsServiceFactory>();
        services.AddScoped<IWordExportDocumentsService, WordExportDocumentService>();
        services.AddScoped<IOrderDocumentsService, OrderDocumentsService>();
    }
}