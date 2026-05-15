using FluentAssertions;
using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Common.Models;
using NUnit.Framework;

namespace Jasmine.FastenerDepartment.Domain.Tests;

[TestFixture]
class PriceTests
{
    [TestCase(22.0)]
    [TestCase(0)]
    public void CreateObject_CorrectPrice_Success(decimal price)
    {
        var result = new Price(price);

        result.Value.Should().Be(price);
    }

    [TestCase(-3.24)]
    public void CreateObject_IncorrectPrice_Failure(decimal price)
    {
        var exception = Assert.Throws<DomainException>(() => new Price(price));

        exception.Should().NotBeNull();
        exception.Code.Should().Be(1002);
        exception.Message.Should().Be("Incorrect price. Price cannot be less than 0.");
    }
}
