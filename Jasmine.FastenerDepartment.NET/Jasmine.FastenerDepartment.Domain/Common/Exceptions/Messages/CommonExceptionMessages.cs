using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Common.Exceptions.Messages;

internal static class CommonExceptionMessages
{
    public static LocalizedString IncorrectName = new(
        "Incorrect name. Name cannot be empty",
        "Некорректное наименование. Наименование не может быть пустым");

    public static LocalizedString IncorrectPrice = new(
        "Incorrect price. Price cannot be less than 0",
        "Некорректная цена. Цена не может быть меньше 0");

    public static LocalizedString IncorrectQuantity = new(
        "Incorrect quantity. Quantity cannot be less than 0",
        "Некорректное количество. Количество не может быть меньше 0");

    public static LocalizedString SpecialMeasurementNotAllowed = new(
        "Special measurement cannot be used with common measurement",
        "Нестандартная единица измерения не может использоваться вместе со стандартной");

    public static LocalizedString MeasurementUnitNotFound = new(
        "Measurement unit not found",
        "Отсутствует единица измерения");
}
