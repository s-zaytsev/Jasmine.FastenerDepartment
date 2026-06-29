using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Common.Exceptions.Messages;

internal static class OrderExceptionMessages
{
    public static LocalizedString EmptyProductList = new(
        "The product list cannot be empty",
        "Список продуктов не может быть пустым");

    public static LocalizedString ChangingStatusNotAllowed = new(
        "The order has already been completed. The status cannot be changed",
        "Заказ уже завершен. Статус заказа не может быть изменен");

    public static LocalizedString OnlyOneCreatedStatus = new(
        "The order can have only one status 'Created'",
        "У заказа может быть только один статус 'Создан'");

    public static LocalizedString ProductsListCannotBeChanged = new(
        "The order has already been completed. The products list cannot be changed",
        "Заказ уже завершен. Список товаров не может быть изменен");

    public static LocalizedString ProductNameCannotBeChanged = new(
        "Existing product name cannot be changed",
        "Наименование существующего товара не может быть изменено");

    public static LocalizedString ProductTypeCannotBeChanged = new(
        "Existing product type cannot be changed",
        "Тип существующего товара не может быть изменен");

    public static LocalizedString SupplierProductNumberCannotBeChanged = new(
        "Existing supplier product number cannot be changed",
        "Артикул существующего товара поставщика не может быть изменен");
}
