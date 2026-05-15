using Jasmine.FastenerDepartment.Application;
using Jasmine.FastenerDepartment.ConsoleApplication.Services;
using Jasmine.FastenerDepartment.Domain;
using Jasmine.FastenerDepartment.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Service is started.");
        var configuration = GetConfiguration();
        var provider = Configure(configuration);

        var jsonProductsService = provider.GetRequiredService<IJsonProductService>();

        try
        {
            await jsonProductsService.ActualizeProductsFromJsonFileAsync(
                configuration.GetRequiredSection("File").Value,
                configuration.GetRequiredSection("LogsDirectory").Value);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadKey();
        }
        Console.WriteLine("Нажмите любую кнопку для закрытия");
        Console.ReadKey();
    }

    private static IServiceProvider Configure(IConfigurationRoot configuration)
    {
        var services = new ServiceCollection();
        services.AddDomainServices(configuration);
        services.AddEFServices();
        //    services.AddInfrastructureServices(configuration);
        services.AddApplicationServices(configuration);

        services.AddTransient<IJsonProductService, JsonProductService>();

        services.AddDbContext<ApplicationDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        var provider = services.BuildServiceProvider();
        //    provider.UseDatabaseMigrations();

        return provider;
    }

    private static IConfigurationRoot GetConfiguration()
    {
        return new ConfigurationBuilder()
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .AddEnvironmentVariables()
          .Build();
    }
}