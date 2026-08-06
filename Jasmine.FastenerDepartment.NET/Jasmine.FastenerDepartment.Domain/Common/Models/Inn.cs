using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Common.Exceptions.Codes;
using Jasmine.FastenerDepartment.Domain.Common.Expression;

namespace Jasmine.FastenerDepartment.Domain.Common.Models;

/// <summary>
/// Individual identification number.
/// </summary>
public class Inn
{
    private const int INDIVIDUAL_ENTREPRENEUR_INN_LENGTH = 12;

    private string _value;

    /// <summary>
    /// Value.
    /// </summary>
    public string Value
    {
        get => _value;
        set
        {
            if (!IsValid(value))
            {
                DomainGuard.ThrowCommonException(CommonExceptionCode.IncorrectInn);
            }

            _value = RegularExpressions.RemoveMultiplySpaces(value).Trim();
        }
    }

    /// <summary>
    /// Creates INN.
    /// </summary>
    private Inn() { }

    /// <summary>
    /// Creates the INN.
    /// </summary>
    /// <param name="inn">Individual identification number.</param>
    public Inn(string inn)
    {
        Value = inn;
    }

    /// <summary>
    /// Checks if INN is valid.
    /// </summary>
    /// <param name="inn">Individual identification number.</param>
    public bool IsValid(string inn)
    {
        return inn.Length == INDIVIDUAL_ENTREPRENEUR_INN_LENGTH;
    }
}
