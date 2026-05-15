using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Common.Exceptions.Codes;

namespace Jasmine.FastenerDepartment.Domain.Products.Models;

/// <summary>
/// Product number.
/// </summary>
public class ProductNumber
{
    private const int MIN_VALUE = 10000001;
    private const int MAX_VALUE = 19999999;

    private int _value;

    /// <summary>
    /// Value.
    /// </summary>
    public int Value 
    {
        get => _value;
        set 
        {
            if (value < MIN_VALUE || value > MAX_VALUE)
            {
                DomainGuard.ThrowProductException(ProductExceptionCode.IncorrectNumber);
            }

            _value = value;
        }
    }

    private ProductNumber() { }

    /// <summary>
    /// Product number.
    /// </summary>
    /// <param name="number">Number.</param>
    public ProductNumber(int number)
    {
        Value = number;
    }
}
