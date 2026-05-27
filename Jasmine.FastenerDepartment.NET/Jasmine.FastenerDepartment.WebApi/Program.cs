using Jasmine.FastenerDepartment.Application;
using Jasmine.FastenerDepartment.Documents;
using Jasmine.FastenerDepartment.Domain;
using Jasmine.FastenerDepartment.EF;
using Jasmine.FastenerDepartment.Messaging;
using Jasmine.FastenerDepartment.Templates;
using Jasmine.FastenerDepartment.WebApi.Configuration;
using Jasmine.FastenerDepartment.WebApi.Middlewares;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

namespace Jasmine.FastenerDepartment.WebApi;

/// <summary>
/// Program.
/// </summary>
public class Program
{
    /// <summary>
    /// Starts application.
    /// </summary>
    /// <param name="args">Arguments.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Configuration.AddDatabaseConfiguration(options => options.UseNpgsql(connectionString));

        builder.Host.UseSerilog();
        SetupLogger(builder);

        builder.Services.AddSingleton(sp => Log.Logger);

        builder.Services.AddDbContextPool<ApplicationDbContext>(opt =>
            opt.UseNpgsql(connectionString));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddWebServices();
        builder.Services.AddDomainServices(builder.Configuration);
        builder.Services.AddEFServices();
        builder.Services.AddDocumentsServices(builder.Configuration);
        builder.Services.AddApplicationServices(builder.Configuration);
        builder.Services.AddTemplatesServices();
        builder.Services.AddMessagingServices(builder.Configuration);

        var app = builder.Build();

        app.UseSerilogRequestLogging();

        //  app.UseMiddleware<MetricsMiddleware>();
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseMiddleware<RequestLoggingMiddleware>();

        Migrate(app);

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(x =>
            {
                x.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
            });
            app.UseSwaggerUI();
        }

        app.UseCors(options =>
        {
            options
                .WithOrigins(builder.Configuration.GetValue<string>("AllowedHosts"))
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("Content-Disposition");
        });

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void Migrate(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
    }

    private static void SetupLogger(WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "Jasmine.FastenerDepartment")
            .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
            .WriteTo.Console(
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
            .WriteTo.File(
                "Logs/Jasmine.FastenerDepartment-.log",
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
            // ��� Grafana Loki
            //     .WriteTo.GrafanaLoki("http://localhost:3100")
            .CreateLogger();
    }
}
