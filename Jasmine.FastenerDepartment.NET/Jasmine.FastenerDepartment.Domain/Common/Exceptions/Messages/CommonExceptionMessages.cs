namespace Jasmine.FastenerDepartment.Domain.Common.Exceptions.Messages;

internal class CommonExceptionMessages
{
    public const string IncorrectName = "Incorrect name. Name cannot be empty.";
    public const string IncorrectPrice = "Incorrect price. Price cannot be less than 0.";
    public const string IncorrectQuantity = "Incorrect quantity. Quantity cannot be less than 0.";
    public const string SpecialMeasurementNotAllowed = "Special measurement cannot be used with common measurement.";
    public const string MeasurementUnitNotFound = "Measurement unit not found.";
}
