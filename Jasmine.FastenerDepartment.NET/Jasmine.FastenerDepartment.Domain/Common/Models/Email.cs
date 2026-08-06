using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Common.Exceptions.Codes;
using Jasmine.FastenerDepartment.Domain.Common.Expression;

namespace Jasmine.FastenerDepartment.Domain.Common.Models;

/// <summary>
/// Email.
/// </summary>
public class Email
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
            if (!RegularExpressions.IsValidEmail(value))
            {
                DomainGuard.ThrowCommonException(CommonExceptionCode.IncorrectEmail);
            }

            _value = value;
        }
    }

    private Email() { }

    /// <summary>
    /// Creates email.
    /// </summary>
    /// <param name="email">Email.</param>
    public Email(string email)
    {
        Value = email;
    }
}
