using FluentAssertions;
using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using NUnit.Framework;

namespace Jasmine.FastenerDepartment.Domain.Tests;

[TestFixture]
class QuantityTests
{
    [TestCase(22, MeasurementUnitCode.Sets, "")]
    [TestCase(0, null, "Special measurement")]
    public void CreateObject_CorrectQuantity_Success(
        double quantity, MeasurementUnitCode? code, string specialMeasurementUnit)
    {
        var result = new Quantity(quantity, code, specialMeasurementUnit);

        result.Value.Should().Be(quantity);
        result.MeasurementUnitCode.Should().Be(code);
        result.SpecialMeasurementUnit.Should().Be(specialMeasurementUnit);
    }

    [TestCase(-22, MeasurementUnitCode.Sets, "")]
    public void CreateObject_IncorrectPrice_Failure(
        double quantity, MeasurementUnitCode? code, string specialMeasurementUnit)
    {
        var exception = Assert.Throws<DomainException>(() => new Quantity(quantity, code, specialMeasurementUnit));

        exception.Should().NotBeNull();
        exception.Code.Should().Be(1003);
        exception.Message.Should().Be("Incorrect quantity. Quantity cannot be less than 0.");
    }

    [TestCase(22, MeasurementUnitCode.Sets, "Special measurement")]
    public void CreateObject_HasCommonAndSpecialMeasurement_Failure(
        double quantity, MeasurementUnitCode? code, string specialMeasurementUnit)
    {
        var exception = Assert.Throws<DomainException>(() => new Quantity(quantity, code, specialMeasurementUnit));

        exception.Should().NotBeNull();
        exception.Code.Should().Be(1004);
        exception.Message.Should().Be("Special measurement cannot be used with common measurement.");
    }

    [TestCase(22, null, "")]
    public void CreateObject_NoMeasurement_Failure(
        double quantity, MeasurementUnitCode? code, string specialMeasurementUnit)
    {
        var exception = Assert.Throws<DomainException>(() => new Quantity(quantity, code, specialMeasurementUnit));

        exception.Should().NotBeNull();
        exception.Code.Should().Be(1005);
        exception.Message.Should().Be("Measurement unit not found.");
    }
}
