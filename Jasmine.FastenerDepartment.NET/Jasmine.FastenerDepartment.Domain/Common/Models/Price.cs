using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Common.Exceptions.Codes;

namespace Jasmine.FastenerDepartment.Domain.Common.Models;

/// <summary>
/// Price.
/// </summary>
public class Price
{
    private decimal _price;

    /// <summary>
    /// Price value.
    /// </summary>
    public decimal Value 
    {
        get => _price;
        set
        {
            if (value < 0)
            {
                DomainGuard.ThrowCommonException(CommonExceptionCode.IncorrectPrice);
            }

            _price = value;
        }
    }

    private Price() { }

    /// <summary>
    /// Creates the price.
    /// </summary>
    /// <param name="price">Price.</param>
    public Price(decimal price)
    {
        Value = price;
    }
}
