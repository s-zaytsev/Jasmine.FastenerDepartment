using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Common.Exceptions.Messages;

internal static class ProductExceptionMessages
{
    public static LocalizedString IncorrectNumber = new(
        "Invalid number. The number must be between 10000001 and 19999999",
        "Невалидный артикул. Артикул должен быть в диапазоне от 10000001 до 19999999");
}
