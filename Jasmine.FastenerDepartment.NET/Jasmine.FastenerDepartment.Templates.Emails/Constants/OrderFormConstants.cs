using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Templates.Emails.Constants;
internal static class OrderFormConstants
{
    internal readonly static LocalizedString ORDER = new("Order", "Заказ");
    internal readonly static LocalizedString FROM = new("from", "от");
    internal readonly static LocalizedString NUMBER = new("Number", "Артикул");
    internal readonly static LocalizedString NAME = new("Name", "Наименование");
    internal readonly static LocalizedString AMOUNT = new("Amount", "Количество");
    internal readonly static LocalizedString ATTACHMENTS_BLOCK = new(
        "You will find more detailed information on the order in the attached files",
        "Более подробную информацию по заказу вы найдёте в прикрепленных файлах");
}
