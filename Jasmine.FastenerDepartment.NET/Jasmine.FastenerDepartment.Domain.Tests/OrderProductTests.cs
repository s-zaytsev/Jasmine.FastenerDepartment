using FluentAssertions;
using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.Orders.Models;
using NUnit.Framework;

namespace Jasmine.FastenerDepartment.Domain.Tests;

[TestFixture]
class OrderProductTests
{
    [Test]
    public void CreateObject_CorrectDataWithGuid_Success()
    {
        var product = new OrderProduct(Guid.NewGuid(), new Quantity(22, MeasurementUnitCode.Pieces));

        product.Should().NotBeNull();
    }

    [Test]
    public void CreateObject_CorrectDataWithName_Success()
    {
        var product = new OrderProduct("Test product", new Quantity(22, MeasurementUnitCode.Pieces));

        product.Should().NotBeNull();
    }

    [Test]
    public void ChangeName_ProductWithoutId_Success()
    {
        var product = new OrderProduct("Test product", new Quantity(22, MeasurementUnitCode.Pieces));
        product.ChangeName("Modified name");

        product.ProductName.Value.Should().Be("Modified name");
    }

    [TestCase]
    public void ChangeName_IncorrectPrice_Failure()
    {
        var product = new OrderProduct(Guid.NewGuid(), new Quantity(22, MeasurementUnitCode.Pieces));

        var exception = Assert.Throws<DomainException>(
            () => product.ChangeName("Modified name"));

        exception.Should().NotBeNull();
        exception.Code.Should().Be(2005);
        exception.Message.Should().Be("Existing product name cannot be changed.");
    }

    [Test]
    public void ChangeQuantity_CorrectData_Success()
    {
        var product = new OrderProduct("Test product", new Quantity(22, MeasurementUnitCode.Pieces));
        product.ChangeOrderedQuantity(new(11, MeasurementUnitCode.Meters));

        product.Ordered.Value.Should().Be(11);
        product.Ordered.MeasurementUnitCode.Should().Be(MeasurementUnitCode.Meters);
    }

    [TestCase]
    public void ChangeQuantity_Null_Failure()
    {
        var product = new OrderProduct(Guid.NewGuid(), new Quantity(22, MeasurementUnitCode.Pieces));

        var exception = Assert.Throws<ArgumentNullException>(
            () => product.ChangeOrderedQuantity(null));

        exception.Should().NotBeNull();
    }
}
