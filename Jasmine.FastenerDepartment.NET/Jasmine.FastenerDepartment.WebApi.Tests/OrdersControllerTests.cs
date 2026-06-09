using FluentAssertions;
using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.Domain.ProductTypes.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Models;
using Jasmine.FastenerDepartment.WebApi.Dtos.Orders;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Jasmine.FastenerDepartment.WebApi.Tests;

[TestFixture]
class OrdersControllerTests
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
    public async Task CanGetPage()
    {
        var orders = new Order[]
        {
            new(1, null, [new("Test 1", new(1, "Test"))]),
            new(2, null, [new("Test 2", new(1, "Test 2"))]),
            new(3, null, [new("Test 3", new(1, "Test 3"))]),
        };

        await DataService.AddOrders(orders);

        var response = await Config.HttpClient.GetStringAsync($"/orders?pageNo=1&pageSize=2");

        var page = JsonConvert.DeserializeObject<Page<OrderDto>>(response);

        page.Should().NotBeNull();
        page.Items.Count.Should().Be(2);
        page.TotalCount.Should().Be(3);
        page.TotalPages.Should().Be(2);
    }

    [Test]
    public async Task CanGetOrder()
    {
        var order = new Order(1, null, [new("Test 1", new(1, "Test"))]);

        await DataService.AddOrders(order);

        var response = await Config.HttpClient.GetStringAsync($"/orders/{order.Id}");

        var orderDto = JsonConvert.DeserializeObject<OrderDto>(response);

        orderDto.Should().NotBeNull();
        orderDto.Products.Count.Should().Be(1);
        orderDto.Number.Should().NotBe("0");
    }

    [Test]
    public async Task CanCreateOrder()
    {
        var product = new Product(
            10000001,
            "First existed product",
            22,
            PriceTagCode.S,
            MeasurementUnitCode.Meters,
            null,
            false,
            true,
            false);

        await DataService.AddProducts([product]);

        var model = new ChangeOrderDto(
        [
            new(null, "Test name", null, new(1, null, "Test"), "123-qwerty"),
            new(product.Id, product.Name.Value, null, new(2, MeasurementUnitCode.Pieces, ""), "")
        ]);

        var json = DataService.GetJsonContent(model);

        var response = await Config.HttpClient.PostAsync($"/orders", json);
        response.EnsureSuccessStatusCode();

        var ordersDb = await Config.CreateDbContext().Orders.Include(x => x.Products).ToListAsync();
        var order = ordersDb.FirstOrDefault();

        ordersDb.Count.Should().Be(1);
        order.Should().NotBeNull();
        order.Number.Should().NotBe(0);
        order.StatusCode.Should().Be(OrderStatusCode.Created);
        order.Products.Count.Should().Be(2);
        order.Products.Count(x => x.ProductId.HasValue).Should().Be(1);
    }

    [Test]
    public async Task CanCompleteOrderWithoutSupplier()
    {
        var productType = new ProductType("Test type");
        await DataService.AddProductTypes(productType);

        var firstProduct = new Product(
            10000001,
            "First existed product",
            22,
            PriceTagCode.S,
            MeasurementUnitCode.Meters,
            null,
            false,
            true,
            false);

        var secondProduct = new Product(
            10000002,
            "Second existed product",
            22,
            PriceTagCode.S,
            MeasurementUnitCode.Meters,
            productType.Id,
            false,
            true,
            false);

        var thirdProduct = new Product(
            10000003,
            "Third existed product",
            22,
            PriceTagCode.S,
            MeasurementUnitCode.Meters,
            null,
            false,
            true,
            false);

        var fourthProduct = new Product(
            10000004,
            "Fourth existed product",
            22,
            PriceTagCode.S,
            MeasurementUnitCode.Meters,
            null,
            false,
            true,
            false);

        await DataService.AddProducts([firstProduct, secondProduct, thirdProduct, fourthProduct]);

        var order = new Order(
            1,
            null,
            [
                new("Test", new(1, "Test")),
                new(firstProduct.Id, firstProduct.Name.Value, null, new(1, MeasurementUnitCode.Meters), ""),
                new(secondProduct.Id, secondProduct.Name.Value, productType.Id, new(2, MeasurementUnitCode.Meters), "")
            ]);

        await DataService.AddOrders([order]);

        var completeRequest = new CompleteOrderDto(
            "Everything is good",
            new List<CompleteOrderProductDto>
            {
                new(
                    order.Products.ElementAt(1).Id,
                    firstProduct.Id,
                    firstProduct.Name.Value,
                    23,
                    null,
                    true,
                    true,
                    true,
                    new(2, MeasurementUnitCode.Meters, ""),
                    ""),
                new(
                    order.Products.Last().Id,
                    secondProduct.Id,
                    secondProduct.Name.Value,
                    23,
                    productType.Id,
                    false,
                    true,
                    true,
                    new(2, MeasurementUnitCode.Meters, ""),
                    "XXX-YYY"),
                new(
                    order.Products.First().Id,
                    null,
                    order.Products.First().ProductName.Value,
                    23,
                    null,
                    true,
                    true,
                    true,
                    new(1, null, "Test"),
                    ""),
                new(
                    null,
                    null,
                    "Extra product",
                    23,
                    productType.Id,
                    true,
                    true,
                    true,
                    new(2, MeasurementUnitCode.Pieces, ""),
                    "417-555"),
                new(
                    null,
                    null,
                    "Not fulfilled extra product",
                    23,
                    null,
                    false,
                    true,
                    true,
                    new(2, MeasurementUnitCode.Pieces, ""),
                    ""),
                new(
                    null,
                    thirdProduct.Id,
                    thirdProduct.Name.Value,
                    777,
                    null,
                    true,
                    true,
                    true,
                    new(2, MeasurementUnitCode.Pieces, ""),
                    ""),
                new(
                    null,
                    fourthProduct.Id,
                    fourthProduct.Name.Value,
                    777,
                    null,
                    false,
                    true,
                    true,
                    new(2, MeasurementUnitCode.Pieces, ""),
                    "")
            });

        var json = DataService.GetJsonContent(completeRequest);
        var response = await Config.HttpClient.PostAsync($"/orders/{order.Id}/complete", json);

        response.EnsureSuccessStatusCode();

        var dbContext = Config.CreateDbContext();

        var ordersDb = await dbContext.Orders.Include(x => x.Products).ToListAsync();
        var orderDb = ordersDb.First();

        var products = await dbContext.Products.ToListAsync();
        var extraProduct = products.FirstOrDefault(x => x.Name.Value == "Extra product");

        ordersDb.Count.Should().Be(1);
        orderDb.StatusCode.Should().Be(OrderStatusCode.Fulfilled);
        orderDb.Products.Count.Should().Be(7);
        orderDb.Products.Count(x => x.IsFulfilled).Should().Be(4);
        orderDb.Products.Count(x => !x.IsFulfilled).Should().Be(3);
        orderDb.Products.Where(x => x.IsFulfilled).All(x => x.ProductId.HasValue).Should().BeTrue();

        products.First(x => x.Id == thirdProduct.Id).Price.Value.Should().Be(777);
        products.First(x => x.Id == fourthProduct.Id).Price.Value.Should().Be(22);

        extraProduct.Should().NotBeNull();
        extraProduct.TypeId.Should().Be(productType.Id);
    }

    [Test]
    public async Task CanCompleteOrderWithSupplier()
    {
        var productType = new ProductType("Test type");
        await DataService.AddProductTypes(productType);

        var supplier = new Supplier("Test supplier", "Test address");
        await DataService.AddSuppliers(supplier);

        var firstProduct = new Product(
            10000001,
            "First existed product",
            22,
            PriceTagCode.S,
            MeasurementUnitCode.Meters,
            null,
            false,
            true,
            false);

        await DataService.AddProducts(firstProduct);

        var order = new Order(
            1,
            supplier.Id,
            [
                new(firstProduct.Id, firstProduct.Name.Value, null, new(1, MeasurementUnitCode.Meters), ""),
            ]);

        await DataService.AddOrders([order]);

        var completeRequest = new CompleteOrderDto(
            "Everything is good",
            new List<CompleteOrderProductDto>
            {
                new(
                    order.Products.First().Id,
                    firstProduct.Id,
                    firstProduct.Name.Value,
                    23,
                    null,
                    true,
                    true,
                    true,
                    new(2, MeasurementUnitCode.Meters, ""),
                    "123-456"),
                new(
                    null,
                    null,
                    "Extra product",
                    23,
                    productType.Id,
                    true,
                    true,
                    true,
                    new(2, MeasurementUnitCode.Pieces, ""),
                    "417-555"),
                new(
                    null,
                    null,
                    "Not fulfilled extra product",
                    23,
                    null,
                    false,
                    true,
                    true,
                    new(2, MeasurementUnitCode.Pieces, ""),
                    "")
            });

        var json = DataService.GetJsonContent(completeRequest);
        var response = await Config.HttpClient.PostAsync($"/orders/{order.Id}/complete", json);

        response.EnsureSuccessStatusCode();

        var dbContext = Config.CreateDbContext();

        var ordersDb = await dbContext.Orders.Include(x => x.Products).ToListAsync();
        var orderDb = ordersDb.First();

        var extraProduct = await dbContext.Products.FirstOrDefaultAsync(x => x.Name.Value == "Extra product");

        var supplierProducts = await dbContext.SupplierProducts.Where(x => x.SupplierId == supplier.Id).ToListAsync();

        ordersDb.Count.Should().Be(1);
        orderDb.StatusCode.Should().Be(OrderStatusCode.Fulfilled);
        orderDb.Products.Count.Should().Be(3);
        orderDb.Products.Count(x => x.IsFulfilled).Should().Be(2);
        orderDb.Products.Count(x => !x.IsFulfilled).Should().Be(1);
        orderDb.Products.Where(x => x.IsFulfilled).All(x => x.ProductId.HasValue).Should().BeTrue();

        extraProduct.Should().NotBeNull();
        extraProduct.TypeId.Should().Be(productType.Id);

        supplierProducts.Count.Should().Be(2);
        supplierProducts.All(x => !string.IsNullOrWhiteSpace(x.Number)).Should().BeTrue();
    }

    [Test]
    public async Task CanCancelOrder()
    {
        var order = new Order(1, null, [new("Test 1", new(1, "Test"))]);

        await DataService.AddOrders(order);

        var model = new CancelOrderDto("Test cancel");
        var json = DataService.GetJsonContent(model);

        var response = await Config.HttpClient.PostAsync($"/orders/{order.Id}/cancel", json);
        response.EnsureSuccessStatusCode();

        var dbContext = Config.CreateDbContext();
        var ordersDb = await dbContext.Orders.ToListAsync();
        var orderDb = ordersDb.First();

        ordersDb.Count.Should().Be(1);
        orderDb.StatusCode.Should().Be(OrderStatusCode.Cancelled);
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
                true,
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
                true,
                false
            ),
        };

        return products;
    }
}
