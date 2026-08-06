using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Common.Exceptions.Codes;
using Jasmine.FastenerDepartment.Domain.Common.Expression;

namespace Jasmine.FastenerDepartment.Domain.Common.Models;

/// <summary>
/// Phone number.
/// </summary>
public class PhoneNumber
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
            if (!RegularExpressions.IsValidPhoneNumber(value))
            {
                DomainGuard.ThrowCommonException(CommonExceptionCode.IncorrectPhoneNumber);
            }

            _value = value;
        }
    }

    private PhoneNumber() { }

    /// <summary>
    /// Creates a phone number.
    /// </summary>
    /// <param name="phoneNumber">Phone number.</param>
    public PhoneNumber(string phoneNumber)
    {
        Value = phoneNumber;
    }
}
