using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Common.Exceptions.Codes;
using Jasmine.FastenerDepartment.Domain.Common.Expression;

namespace Jasmine.FastenerDepartment.Domain.Common.Models;

/// <summary>
/// Name.
/// </summary>
public class Name
{
    private string _value;

    /// <summary>
    /// Value.
    /// </summary>
    public string Value
    {
        get => _value;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                DomainGuard.ThrowCommonException(CommonExceptionCode.IncorrectName);
            }

            _value = RegularExpressions.RemoveMultiplySpaces(value).Trim();
        }
    }

    private Name() { }

    /// <summary>
    /// Creates the name.
    /// </summary>
    /// <param name="name">Name.</param>
    public Name(string name)
    {
        Value = name;
    }
}
