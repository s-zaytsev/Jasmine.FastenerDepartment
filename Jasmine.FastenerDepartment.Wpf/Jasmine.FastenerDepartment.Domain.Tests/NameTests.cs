using FluentAssertions;
using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Common.Models;
using NUnit.Framework;

namespace Jasmine.FastenerDepartment.Domain.Tests;

[TestFixture]
class NameTests
{
    [TestCase("New name")]
    public void CreateObject_CorrectName_Success(string name)
    {
        var result = new Name(name);

        result.Value.Should().Be("New name");
    }

    [TestCase("     New     name     ")]
    public void CreateObject_CorrectNameWithExtraWhiteSpaces_Success(string name)
    {
        var result = new Name(name);

        result.Value.Should().Be("New name");
    }

    [TestCase("")]
    public void CreateObject_IncorrectName_Failure(string name)
    {
        var exception = Assert.Throws<DomainException>(() => new Name(name));

        exception.Should().NotBeNull();
        exception.Code.Should().Be(1001);
        exception.Message.Should().Be("Incorrect name. Name cannot be empty.");
    }
}
