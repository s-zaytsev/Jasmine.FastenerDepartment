using FluentAssertions;
using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using NUnit.Framework;

namespace Jasmine.FastenerDepartment.Domain.Tests;

[TestFixture]
class ProductNumberTests
{
    [TestCase(10000001)]
    [TestCase(10005000)]
    [TestCase(19999999)]
    public void CreateObject_CorrectNumber_Success(int number)
    {
        var result = new ProductNumber(number);

        result.Value.Should().Be(number);
    }

    [TestCase(-10000001)]
    [TestCase(5000)]
    [TestCase(10000000)]
    public void CreateObject_IncorrectNumber_Failure(int number)
    {
        var exception = Assert.Throws<DomainException>(() => new ProductNumber(number));

        exception.Should().NotBeNull();
        exception.Code.Should().Be(3001);
        exception.Message.Should().Be("Invalid number. The number must be between 10000001 and 19999999.");
    }

}
