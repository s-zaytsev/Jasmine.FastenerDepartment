using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Templates.Constants;
internal static class OrderConstants
{
    internal readonly static LocalizedString ORDER = new("Order", "Заказ");
    internal readonly static LocalizedString FROM = new("from", "от");
    internal readonly static LocalizedString NUMBER = new("Number", "Артикул");
    internal readonly static LocalizedString NAME = new("Name", "Наименование");
    internal readonly static LocalizedString AMOUNT = new("Amount", "Количество");
    internal readonly static LocalizedString ATTACHMENTS_BLOCK = new(
        "You will find more detailed information on the order in the attached files",
        "Более подробную информацию по заказу вы найдёте в прикрепленных файлах");

    internal readonly static IDictionary<int, LocalizedString> POSITIONS = new Dictionary<int, LocalizedString>
    {
        {0, new("positions", "позиций") },
        {1, new("position", "позиция") },
        {2, new("positions", "позиции") },
        {3, new("positions", "позиции") },
        {4, new("positions", "позиции") },
        {5, new("positions", "позиций") },
        {6, new("positions", "позиций") },
        {7, new("positions", "позиций") },
        {8, new("positions", "позиций") },
        {9, new("positions", "позиций") },
    };

    internal static LocalizedString GetPositionText(int positions)
    {
        var count = positions % 10;
        return POSITIONS[count];
    }
}
