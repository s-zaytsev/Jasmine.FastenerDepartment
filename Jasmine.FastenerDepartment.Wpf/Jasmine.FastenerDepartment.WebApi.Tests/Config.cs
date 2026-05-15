using Jasmine.FastenerDepartment.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Net.Http.Headers;

namespace Jasmine.FastenerDepartment.WebApi.Tests;

[SetUpFixture]
public class Config
{
    private static IConfiguration _configuration;

    public static ApplicationDbContext DbContext;
    public static HttpClient HttpClient;

    [OneTimeSetUp]
    public static void SetUpFixture()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var webAppFactory = new CustomWebApplicationFactory<Program>();
        HttpClient = webAppFactory.CreateClient();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

        DbContext = CreateDbContext();
    }

    [OneTimeTearDown]
    public static void OneTimeTearDown()
    {
        DbContext?.Dispose();
        HttpClient?.Dispose();
    }

    public static ApplicationDbContext CreateDbContext()
    {
        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .EnableSensitiveDataLogging()
            .UseNpgsql(_configuration.GetConnectionString("DefaultConnection"))
            .Options;

        return new ApplicationDbContext(dbContextOptions);
    }
}
