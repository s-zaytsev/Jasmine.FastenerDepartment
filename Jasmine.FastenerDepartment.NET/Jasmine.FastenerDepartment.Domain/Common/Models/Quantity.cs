using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Common.Exceptions.Codes;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;

namespace Jasmine.FastenerDepartment.Domain.Common.Models;

/// <summary>
/// Quantity.
/// </summary>
public record Quantity
{
    private double _quantity;
    private MeasurementUnitCode? _measurementUnitCode;
    private string _specialMeasurementUnit;

    /// <summary>
    /// Quantity value.
    /// </summary>
    public double Value
    {
        get => _quantity;
        set
        {
            if (value < 0)
            {
                DomainGuard.ThrowCommonException(CommonExceptionCode.IncorrectQuantity);
            }

            _quantity = value;
        }
    }

    /// <summary>
    /// Measurement unit code.
    /// </summary>
    public MeasurementUnitCode? MeasurementUnitCode
    {
        get => _measurementUnitCode;
        set => _measurementUnitCode = value;
    }

    /// <summary>
    /// Special measurement unit code, which doesn't exists in system.
    /// </summary>
    public string SpecialMeasurementUnit
    {
        get => _specialMeasurementUnit;
        set
        {
            if (_measurementUnitCode.HasValue && !string.IsNullOrWhiteSpace(value))
            {
                DomainGuard.ThrowCommonException(CommonExceptionCode.SpecialMeasurementNotAllowed);
            }

            if (!_measurementUnitCode.HasValue && string.IsNullOrWhiteSpace(value))
            {
                DomainGuard.ThrowCommonException(CommonExceptionCode.MeasurementUnitNotFound);
            }

            _specialMeasurementUnit = value;
        }
    }

    private Quantity() { }

    /// <summary>
    /// Creates quantity.
    /// </summary>
    /// <param name="quantity">Quantity.</param>
    /// <param name="measurementUnitCode">Measurement unit code.</param>
    /// <param name="specialMeasurementUnit">Special measurement unit.</param>
    public Quantity(
        double quantity,
        MeasurementUnitCode? measurementUnitCode,
        string specialMeasurementUnit)
    {
        Value = quantity;
        MeasurementUnitCode = measurementUnitCode;
        SpecialMeasurementUnit = specialMeasurementUnit;
    }

    /// <summary>
    /// Creates quantity.
    /// </summary>
    /// <param name="quantity">Quantity.</param>
    /// <param name="specialMeasurementUnit">Special measurement unit.</param>
    public Quantity(
        double quantity,
        string specialMeasurementUnit)
    {
        Value = quantity;
        SpecialMeasurementUnit = specialMeasurementUnit;
    }

    /// <summary>
    /// Creates quantity.
    /// </summary>
    /// <param name="quantity">Quantity.</param>
    /// <param name="measurementUnitCode">Measurement unit code.</param>
    public Quantity(
        double quantity,
        MeasurementUnitCode? measurementUnitCode)
    {
        Value = quantity;
        MeasurementUnitCode = measurementUnitCode;
    }
}
