using FluentAssertions;
using Jasmine.FastenerDepartment.Application.Models.Synchronization;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.WebApi.Dtos.Synchronization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Jasmine.FastenerDepartment.WebApi.Tests;

[TestFixture]
public class SynchronizationControllerTests
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
    public async Task CanSyncFirstTime()
    {
        var products = GetServerProducts();
        await DataService.AddProducts([.. products]);

        var request = new SynchronizationRequestDto(null, []);

        var json = DataService.GetJsonContent(request);

        var response = await Config.HttpClient.PostAsync($"/synchronization", json);
        response.EnsureSuccessStatusCode();

        var responseText = await response.Content.ReadAsStringAsync();
        var model = JsonConvert.DeserializeObject<SynchronizationResponse>(responseText);

        model.Should().NotBeNull();
        model.NewProducts.Count.Should().Be(3);
    }

    [Test]
    public async Task CanSyncWithDateWithoutClientModifiedProducts()
    {
        var products = GetServerProducts();
        await DataService.AddProducts([.. products]);

        var request = new SynchronizationRequestDto(DateTime.UtcNow.AddDays(-2), []);

        var json = DataService.GetJsonContent(request);

        var response = await Config.HttpClient.PostAsync($"/synchronization", json);

        var responseText = await response.Content.ReadAsStringAsync();
        var model = JsonConvert.DeserializeObject<SynchronizationResponseDto>(responseText);

        model.Should().NotBeNull();
        model.NewProducts.Count().Should().Be(2);
    }

    [Test]
    public async Task CanSyncWithDateWithNewProducts()
    {
        var products = GetServerProducts();
        await DataService.AddProducts([.. products]);

        var clientProducts = new List<SynchronizationProductDto> {
                new(
                    Guid.NewGuid(),
                    10000005,
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    "New Product",
                    24,
                    false,
                    false,
                    false,
                    MeasurementUnitCode.Pieces,
                    PriceTagCode.L)
                };

        var request = new SynchronizationRequestDto(DateTime.UtcNow.AddDays(-2), clientProducts);

        var json = DataService.GetJsonContent(request);
        var response = await Config.HttpClient.PostAsync($"/synchronization", json);

        var responseText = await response.Content.ReadAsStringAsync();
        var model = JsonConvert.DeserializeObject<SynchronizationResponseDto>(responseText);

        var productsDb = await Config.CreateDbContext().Products.ToListAsync();

        model.Should().NotBeNull();
        model.NewProducts.Count().Should().Be(2);
        productsDb.Count.Should().Be(4);
    }

    [Test]
    public async Task CanSyncWithDateWithClientModifiedProducts()
    {
        var products = GetServerProducts();
        await DataService.AddProducts([.. products]);

        var clientProducts = new List<SynchronizationProductDto> {
                new(
                    products.MinBy(x => x.CreatedDate).Id,
                    10000005,
                    products.MinBy(x => x.CreatedDate).CreatedDate,
                    DateTime.UtcNow.AddMinutes(-10),
                    "Modified Product",
                    24,
                    false,
                    false,
                    false,
                    MeasurementUnitCode.Pieces,
                    PriceTagCode.L)
                };

        var request = new SynchronizationRequestDto(DateTime.UtcNow.AddDays(-2), clientProducts);


        var json = DataService.GetJsonContent(request);
        var response = await Config.HttpClient.PostAsync($"/synchronization", json);

        var responseText = await response.Content.ReadAsStringAsync();
        var model = JsonConvert.DeserializeObject<SynchronizationResponseDto>(responseText);

        var productsDb = await Config.CreateDbContext().Products.ToListAsync();
        var modifiedProduct = productsDb.First(x => x.Id == request.Products.First().Id);

        model.Should().NotBeNull();
        model.NewProducts.Count().Should().Be(2);
        productsDb.Count.Should().Be(3);
        modifiedProduct.Name.Value.Should().Be("Modified Product");
    }

    [Test]
    public async Task CanSyncWithDateWithClientNewProductAndServerModifiedProducts()
    {
        var products = GetServerProducts();

        var serverProduct1 = new Product(
            DateTime.UtcNow.AddDays(-999),
            DateTime.UtcNow.AddDays(-1),
            10000009,
            "Modified on server",
            999,
            PriceTagCode.S,
            MeasurementUnitCode.Pieces,
            false,
            false,
            false
        );

        serverProduct1.AddHistoryEntry(ProductChangeReasonCode.ChangedPrice, 2, serverProduct1.Price, serverProduct1.ModifiedDate);

        products.Add(serverProduct1);

        await DataService.AddProducts([.. products]);

        var clientProducts = new List<SynchronizationProductDto> {
            new(
                Guid.NewGuid(),
                10000005,
                DateTime.UtcNow.AddHours(-1),
                DateTime.UtcNow.AddHours(-1),
                "New Product For Server",
                24,
                false,
                false,
                false,
                MeasurementUnitCode.Pieces,
                PriceTagCode.L)
            };

        var request = new SynchronizationRequestDto(DateTime.UtcNow.AddDays(-2), clientProducts);

        var json = DataService.GetJsonContent(request);
        var response = await Config.HttpClient.PostAsync($"/synchronization", json);

        var responseText = await response.Content.ReadAsStringAsync();
        var model = JsonConvert.DeserializeObject<SynchronizationResponseDto>(responseText);

        var productsDb = await Config.CreateDbContext().Products.ToListAsync();
        var addedProduct = productsDb.First(x => x.Id == request.Products.First().Id);

        model.Should().NotBeNull();
        model.NewProducts.Count().Should().Be(2);
        model.ModifiedProducts.Count().Should().Be(1);
        model.HistoryEntries.Count().Should().Be(2);
        productsDb.Count.Should().Be(5);
        addedProduct.Name.Value.Should().Be("New Product For Server");
    }

    [Test]
    public async Task CanSyncFullFlow()
    {
        var products = GetServerProducts();

        var serverProduct1 = new Product(
            DateTime.UtcNow.AddDays(-999),
            DateTime.UtcNow.AddDays(-1),
            10000009,
            "Modified on server after the last synchronization",
            999,
            PriceTagCode.S,
            MeasurementUnitCode.Pieces,
            false,
            false,
            false
        );

        var serverProduct2 = new Product(
            DateTime.UtcNow.AddDays(-1),
            DateTime.UtcNow.AddDays(-1),
            10000007,
            "Added after the last synchronization",
            987,
            PriceTagCode.S,
            MeasurementUnitCode.Pieces,
            false,
            false,
            false
        );

        serverProduct1.AddHistoryEntry(ProductChangeReasonCode.ChangedPrice, 2, serverProduct1.Price, serverProduct1.ModifiedDate);

        products.Add(serverProduct1);
        products.Add(serverProduct2);

        await DataService.AddProducts([.. products]);

        var clientProducts = new List<SynchronizationProductDto> {
                new(
                    Guid.NewGuid(),
                    10000001,
                    DateTime.UtcNow.AddHours(-1),
                    DateTime.UtcNow.AddHours(-1),
                    "New Product For Server",
                    24,
                    false,
                    false,
                    false,
                    MeasurementUnitCode.Pieces,
                    PriceTagCode.L),
                new(
                    products.MaxBy(x => x.CreatedDate).Id,
                    10000006,
                    products.MaxBy(x => x.CreatedDate).CreatedDate,
                    DateTime.UtcNow.AddMinutes(-10),
                    "Modified Product",
                    24,
                    false,
                    false,
                    false,
                    MeasurementUnitCode.Pieces,
                    PriceTagCode.L)
                };

        var request = new SynchronizationRequestDto(DateTime.UtcNow.AddDays(-2), clientProducts);

        var json = DataService.GetJsonContent(request);
        var response = await Config.HttpClient.PostAsync($"/synchronization", json);

        var responseText = await response.Content.ReadAsStringAsync();
        var model = JsonConvert.DeserializeObject<SynchronizationResponseDto>(responseText);

        var productsDb = await Config.CreateDbContext().Products.ToListAsync();
        var addedProduct = productsDb.First(x => x.Id == request.Products.First().Id);
        var modifiedProduct = productsDb.First(x => x.Id == request.Products.ElementAt(1).Id);

        model.Should().NotBeNull();
        model.NewProducts.Count().Should().Be(3);
        model.ModifiedProducts.Count().Should().Be(1);
        model.HistoryEntries.Count().Should().Be(2);
        model.HistoryEntries.SelectMany(x => x.HistoryItems).SelectMany(x => x.ProductHistoryEntries).Count().Should().Be(6);
        productsDb.Count.Should().Be(6);
        addedProduct.Name.Value.Should().Be("New Product For Server");
        modifiedProduct.Name.Value.Should().Be("Modified Product");
    }

    private ICollection<Product> GetServerProducts()
    {
        var products = new List<Product>()
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
