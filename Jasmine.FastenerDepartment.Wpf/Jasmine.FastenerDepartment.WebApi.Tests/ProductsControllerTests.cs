using FluentAssertions;
using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Models;
using Jasmine.FastenerDepartment.WebApi.Dtos.Products;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Jasmine.FastenerDepartment.WebApi.Tests;

[TestFixture]
public class ProductsControllerTests
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
    public async Task CanGetProductsPage()
    {
        var products = GetServerProducts();
        await DataService.AddProducts([.. products]);

        var response = await Config.HttpClient.GetStringAsync($"/products?pageNo=1&pageSize=2");

        var page = JsonConvert.DeserializeObject<Page<ProductDto>>(response);

        page.Should().NotBeNull();
        page.Items.Count.Should().Be(2);
        page.TotalCount.Should().Be(3);
        page.TotalPages.Should().Be(2);
    }

    [Test]
    public async Task CanGetProductsPageWithSuppliers()
    {
        var products = GetServerProducts();
        await DataService.AddProducts([.. products]);

        var supplier = new Supplier("Test", "Test address");
        supplier.AddProductById(products.First().Id);

        await DataService.AddSuppliers([supplier]);

        var response = await Config.HttpClient.GetStringAsync($"/products?pageNo=1&pageSize=3");

        var page = JsonConvert.DeserializeObject<Page<ProductDto>>(response);

        page.Should().NotBeNull();
        page.Items.Count.Should().Be(3);
        page.TotalCount.Should().Be(3);
        page.TotalPages.Should().Be(1);
        page.Items.First(x => x.Id == products.First().Id).Suppliers.Should().NotBeEmpty();
        page.Items.First(x => x.Id != products.First().Id).Suppliers.Should().BeEmpty();
    }

    [Test]
    public async Task CanGetProductById()
    {
        var products = GetServerProducts();
        await DataService.AddProducts([.. products]);

        var response = await Config.HttpClient.GetStringAsync($"/products/{products.Last().Id}");

        var product = JsonConvert.DeserializeObject<ProductDto>(response);

        product.Should().NotBeNull();
        product.Number.Should().Be(products.Last().Number.Value);
    }

    [Test]
    public async Task CanGetLastNumber()
    {
        var products = GetServerProducts();
        await DataService.AddProducts([.. products]);

        var response = await Config.HttpClient.GetStringAsync($"/products/last-number");

        var product = JsonConvert.DeserializeObject<int>(response);

        product.Should().Be(10000003);
    }

    [Test]
    public async Task CanCreate()
    {
        var products = GetServerProducts();
        await DataService.AddProducts([.. products]);

        var model = new ChangeProductModelDto(
            "Created by test",
            1,
            null,
            false,
            false,
            false,
            PriceTagCode.S,
            MeasurementUnitCode.Pieces,
            []);

        var json = DataService.GetJsonContent(model);

        var response = await Config.HttpClient.PostAsync($"/products", json);
        response.EnsureSuccessStatusCode();

        var productsDb = await Config.CreateDbContext().Products.ToListAsync();
        var createdProduct = productsDb.FirstOrDefault(x => x.Name.Value == "Created by test");

        productsDb.Count.Should().Be(4);
        createdProduct.Should().NotBeNull();
        createdProduct.Number.Value.Should().Be(10000004);
    }

    [Test]
    public async Task CanUpdate()
    {
        var products = GetServerProducts();
        await DataService.AddProducts([.. products]);

        var request = new ChangeProductModelDto(
            "Updated by test",
            1,
            null,
            false,
            false,
            false,
            PriceTagCode.S,
            MeasurementUnitCode.Pieces,
            []);

        var json = DataService.GetJsonContent(request);

        var response = await Config.HttpClient.PutAsync($"/products/{products.First().Id}", json);
        response.EnsureSuccessStatusCode();

        var productsDb = await Config.CreateDbContext().Products.ToListAsync();

        productsDb.Count.Should().Be(3);
        productsDb.Any(x => x.Name.Value == "Updated by test").Should().BeTrue();
    }

    [Test]
    public async Task CanAddToPrint()
    {
        var products = GetServerProducts();

        var product = new Product(
            10000004,
            "Test",
            100,
            PriceTagCode.L,
            MeasurementUnitCode.Pieces
        );

        products.Add(product);

        await DataService.AddProducts([.. products]);

        var response = await Config.HttpClient.PostAsync($"/products/{product.Id}/print", null);
        response.EnsureSuccessStatusCode();

        var dbContext = Config.CreateDbContext();
        var productsDb = await dbContext.Products.Include(x => x.HistoryEntries).ToListAsync();
        var productsToPrint = productsDb.Where(x => x.IsNeededToPrint);

        productsDb.Count.Should().Be(4);
        productsToPrint.Count().Should().Be(1);
        productsToPrint.First().HistoryEntries.Count.Should().Be(2);
        productsToPrint.First().HistoryEntries.OrderByDescending(x => x.CreatedDate).First().ChangeReasonCode.Should().Be(ProductChangeReasonCode.ChangedPrintStatus);
    }

    [Test]
    public async Task CanRemoveFromPrint()
    {
        using var context = Config.CreateDbContext();
        var products = GetServerProducts();

        var product = new Product
        (
            10000004,
            "Test",
            100,
            PriceTagCode.L,
            MeasurementUnitCode.Pieces,
            null,
            false,
            false,
            true
        );

        products.Add(product);

        await DataService.AddProducts([..products]);

        var response = await Config.HttpClient.PostAsync($"/products/{product.Id}/print", null);
        response.EnsureSuccessStatusCode();

        var productsDb = await context.Products.Include(x => x.HistoryEntries).ToListAsync();
        var productsToPrint = await context.Products.Where(x => x.IsNeededToPrint).ToListAsync();

        var historyEntry = productsDb.First(x => x.Id == product.Id).HistoryEntries.OrderByDescending(x => x.CreatedDate).First();

        productsDb.Count.Should().Be(4);
        productsToPrint.Count.Should().Be(0);
        historyEntry.ChangeReasonCode.Should().Be(ProductChangeReasonCode.ChangedPrintStatus);
        historyEntry.OldValue.Should().Be("True");
        historyEntry.NewValue.Should().Be("False");
    }

    [Test]
    public async Task CanAddToOrder()
    {
        var products = GetServerProducts();

        var product = new Product
        (
            10000004,
            "Test",
            100,
            PriceTagCode.L,
            MeasurementUnitCode.Pieces
        );

        products.Add(product);

        await DataService.AddProducts([.. products]);

        var response = await Config.HttpClient.PostAsync($"/products/{product.Id}/order", null);
        response.EnsureSuccessStatusCode();

        var dbContext = Config.CreateDbContext();
        var productsDb = await dbContext.Products.Include(x => x.HistoryEntries).ToListAsync();
        var productsToOrder = productsDb.Where(x => x.IsNeededToOrder);

        productsDb.Count.Should().Be(4);
        productsToOrder.Count().Should().Be(1);
        productsToOrder.First().Id.Should().Be(product.Id);
        productsDb.First(x => x.Id == product.Id).HistoryEntries.Count.Should().Be(2);
        productsDb.First(x => x.Id == product.Id).HistoryEntries
            .First(x => x.ChangeReasonCode == ProductChangeReasonCode.ChangedOrderStatus).NewValue.Should().Be("True");
    }

    [Test]
    public async Task CanRemoveFromOrder()
    {
        var products = GetServerProducts();

        var product = new Product
        (
            10000004,
            "Test",
            100,
            PriceTagCode.L,
            MeasurementUnitCode.Pieces,
            null,
            false,
            true,
            false
        );

        products.Add(product);

        await DataService.AddProducts([.. products]);

        var response = await Config.HttpClient.PostAsync($"/products/{product.Id}/order", null);
        response.EnsureSuccessStatusCode();

        var dbContext = Config.CreateDbContext();
        var productsDb = await dbContext.Products.Include(x => x.HistoryEntries).ToListAsync();
        var productsToOrder = productsDb.Where(x => x.IsNeededToOrder);

        productsDb.Count.Should().Be(4);
        productsToOrder.Count().Should().Be(0);
        productsDb.First(x => x.Id == product.Id).HistoryEntries.Count.Should().Be(2);
        productsDb
            .First(x => x.Id == product.Id)
            .HistoryEntries
            .First(x => x.ChangeReasonCode == ProductChangeReasonCode.ChangedOrderStatus)
            .NewValue
            .Should()
            .Be("False");
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
