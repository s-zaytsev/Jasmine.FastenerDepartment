namespace Jasmine.FastenerDepartment.Domain.Common.Exceptions.Messages;

internal class OrderExceptionMessages
{
    public const string EmptyProductList = "The product list cannot be empty.";
    public const string ChangingStatusNotAllowed = "The order has already been completed. The status cannot be changed.";
    public const string OnlyOneCreatedStatus = "The order can have only one status 'Created'.";
    public const string ProductsListCannotBeChanged = "The order has already been completed. The products list cannot be changed.";
    public const string ProductNameCannotBeChanged = "Existing product name cannot be changed.";
    public const string ProductTypeCannotBeChanged = "Existing product type cannot be changed.";
    public const string SupplierProductNumberCannotBeChanged = "Existing supplier product number cannot be changed.";
}
