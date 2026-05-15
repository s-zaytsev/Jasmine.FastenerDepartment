using FluentAssertions;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.WebApi.Dtos.HistoryEntries;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Jasmine.FastenerDepartment.WebApi.Tests;

[TestFixture]
class ProductHistoryControllerTests
{
    [SetUp]
    public async Task SetUpAsync()
    {
        await DataService.ClearAsync();
    }

    [TearDown]
    public async Task TearDownAsync()
    {
        await DataService.ClearAsync();
    }

    [Test]
    public async Task CanGetHistory()
    {
        var products = GetServerProducts();
        await DataService.AddProducts(products);

        var response = await Config.HttpClient.GetStringAsync($"/product-history");

        var history = JsonConvert.DeserializeObject<IEnumerable<DailyHistoryDto>>(response);

        history.Should().NotBeNull();
        history.Count().Should().Be(3);
    }

    private Product[] GetServerProducts()
    {
        var products = new Product[]
        {
            new(
                DateTime.UtcNow.AddDays(-1),
                DateTime.UtcNow.AddDays(-1),
                10000001,
                "Test 1",
                1,
                PriceTagCode.S,
                MeasurementUnitCode.Pieces,
                false,
                false,
                false
            ),
            new(
                DateTime.UtcNow.AddDays(-2),
                DateTime.UtcNow.AddDays(-2),
                10000002,
                "Test 2",
                2,
                PriceTagCode.S,
                MeasurementUnitCode.Pieces,
                false,
                false,
                false
            ),
            new(
                DateTime.UtcNow.AddDays(-3),
                DateTime.UtcNow.AddDays(-3),
                10000003,
                "Test 3",
                3,
                PriceTagCode.S,
                MeasurementUnitCode.Pieces,
                false,
                false,
                false
            ),
        };

        return products;
    }
}
