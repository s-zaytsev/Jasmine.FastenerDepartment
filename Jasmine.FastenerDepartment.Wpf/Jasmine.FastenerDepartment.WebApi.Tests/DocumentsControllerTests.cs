using FluentAssertions;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using NUnit.Framework;
using System.Net;

namespace Jasmine.FastenerDepartment.WebApi.Tests;

[TestFixture]
class DocumentsControllerTests
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
    public async Task CanGetWordDocument()
    {
        var products = GetServerProducts();
        await DataService.AddProducts(products);

        var response = await Config.HttpClient.GetAsync($"/documents?documentType=1");

        var responseString = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseString.Should().NotBeNullOrWhiteSpace();
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

        foreach (var product in products)
        {
            product.AddHistoryEntry(ProductChangeReasonCode.Created, "", "", DateTime.UtcNow.AddDays(-1));
        }

        return products;
    }
}
