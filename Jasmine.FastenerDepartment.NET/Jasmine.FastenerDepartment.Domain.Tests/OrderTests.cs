using FluentAssertions;
using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.Orders.Models;
using NUnit.Framework;

namespace Jasmine.FastenerDepartment.Domain.Tests;

[TestFixture]
class OrderTests
{
    [Test]
    public void CreateObject_CorrectOrder_Success()
    {
        var products = new List<OrderProduct>()
        {
            new(Guid.NewGuid(), new(22, MeasurementUnitCode.Pieces)),
            new("Test product", new(12, "Special measurement unit"))
        };

        var result = new Order(1, null, products);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(OrderStatusCode.Created);
        result.HistoryEntries.Should().HaveCount(1);
    }

    [Test]
    public void CreateObject_IncorrectPrice_Failure()
    {
        var exception = Assert.Throws<DomainException>(() => new Order(1, null, []));

        exception.Should().NotBeNull();
        exception.Code.Should().Be(2001);
        exception.Message.Should().Be("The product list cannot be empty.");
    }

    [TestCase(OrderStatusCode.Cancelled)]
    [TestCase(OrderStatusCode.Fulfilled)]
    [TestCase(OrderStatusCode.Sent)]
    public void ChangeStatus_Status_Success(OrderStatusCode statusCode)
    {
        var products = new List<OrderProduct>()
        {
            new(Guid.NewGuid(), new(22, MeasurementUnitCode.Pieces)),
            new("Test product", new(12, "Special measurement unit"))
        };

        var result = new Order(1, null, products);

        result.ChangeStatus(statusCode);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(statusCode);
        result.HistoryEntries.Should().HaveCount(2);
    }

    [TestCase(OrderStatusCode.Cancelled)]
    [TestCase(OrderStatusCode.Created)]
    [TestCase(OrderStatusCode.Fulfilled)]
    [TestCase(OrderStatusCode.Sent)]
    public void DeliveredOrder_ChangeStatus_Status_Failure(OrderStatusCode statusCode)
    {
        var products = new List<OrderProduct>
        {
            new(Guid.NewGuid(), new(22, MeasurementUnitCode.Pieces)),
            new("Test product", new(12, "Special measurement unit"))
        };

        var order = new Order(1, null, products);
        order.ChangeStatus(OrderStatusCode.Fulfilled);


        var exception = Assert.Throws<DomainException>(() => order.ChangeStatus(statusCode));

        exception.Should().NotBeNull();
        exception.Code.Should().Be(2002);
        exception.Message.Should().Be("The order has already been completed. The status cannot be changed.");
    }

    [TestCase(OrderStatusCode.Cancelled)]
    [TestCase(OrderStatusCode.Created)]
    [TestCase(OrderStatusCode.Fulfilled)]
    [TestCase(OrderStatusCode.Sent)]
    public void CancelledOrder_ChangeStatus_Status_Failure(OrderStatusCode statusCode)
    {
        var products = new List<OrderProduct>
        {
            new(Guid.NewGuid(), new(22, MeasurementUnitCode.Pieces)),
            new("Test product", new(12, "Special measurement unit"))
        };

        var order = new Order(1, null, products);
        order.ChangeStatus(OrderStatusCode.Cancelled);


        var exception = Assert.Throws<DomainException>(() => order.ChangeStatus(statusCode));

        exception.Should().NotBeNull();
        exception.Code.Should().Be(2002);
        exception.Message.Should().Be("The order has already been completed. The status cannot be changed.");
    }

    [Test]
    public void CreatedOrder_ChangeStatus_CreatedStatus_Failure()
    {
        var products = new List<OrderProduct>
        {
            new(Guid.NewGuid(), new(22, MeasurementUnitCode.Pieces)),
            new("Test product", new(12, "Special measurement unit"))
        };

        var order = new Order(1, null, products);


        var exception = Assert.Throws<DomainException>(() => order.ChangeStatus(OrderStatusCode.Created));

        exception.Should().NotBeNull();
        exception.Code.Should().Be(2003);
        exception.Message.Should().Be("The order can have only one status 'Created'.");
    }

    [Test]
    public void ChangeProducts_Products_Success()
    {
        var products = new List<OrderProduct>
        {
            new(Guid.NewGuid(), new(22, MeasurementUnitCode.Pieces)),
            new("Test product", new(12, "Special measurement unit"))
        };

        var order = new Order(1, null, products);
        products.Add(new("Extra product`", new(2, MeasurementUnitCode.Pieces)));

        order.ChangeProducts(products);

        order.Should().NotBeNull();
        order.Products.Should().HaveCount(3);
    }


    [Test]
    public void CompletedOrder_ChangeProducts_Products_Failure()
    {
        var products = new List<OrderProduct>
        {
            new(Guid.NewGuid(), new(22, MeasurementUnitCode.Pieces)),
            new("Test product", new(12, "Special measurement unit"))
        };

        var order = new Order(1, null, products);
        order.ChangeStatus(OrderStatusCode.Fulfilled);

        products.Add(new("Extra product`", new(2, MeasurementUnitCode.Pieces)));

        var exception = Assert.Throws<DomainException>(() => order.ChangeProducts(products));

        exception.Should().NotBeNull();
        exception.Code.Should().Be(2004);
        exception.Message.Should().Be("The order has already been completed. The products list cannot be changed.");
    }

    [Test]
    public void ChangeProducts_EmptyList_Failure()
    {
        var products = new List<OrderProduct>
        {
            new(Guid.NewGuid(), new(22, MeasurementUnitCode.Pieces)),
            new("Test product", new(12, "Special measurement unit"))
        };

        var order = new Order(1, null, products);
        var exception = Assert.Throws<DomainException>(() => order.ChangeProducts([]));

        exception.Should().NotBeNull();
        exception.Code.Should().Be(2001);
        exception.Message.Should().Be("The product list cannot be empty.");
    }
}
