using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Templates.Emails.Constants;

internal static class ProductCatalogConstants
{
    internal readonly static LocalizedString NAME = new("Name", "Наименование");
    internal readonly static LocalizedString AMOUNT = new("Amount", "Количество");
    internal readonly static LocalizedString PRODUCT_CATALOG = new("Product catalog", "Каталог товаров");
    internal readonly static LocalizedString MESSAGE_TEXT = new(
        "We present our product catalog. In it, you can find products from the following categories:",
        "Предсталяем Вашему вниманию наш каталог товаров. В нём вы можете найти товары следующих категорий:");
}
