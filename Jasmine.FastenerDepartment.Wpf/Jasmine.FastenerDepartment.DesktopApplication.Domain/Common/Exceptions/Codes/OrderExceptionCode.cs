namespace Jasmine.FastenerDepartment.Domain.Common.Exceptions.Codes;

internal enum OrderExceptionCode
{
    EmptyProductList = 2001,
    ChangingStatusNotAllowed = 2002,
    OnlyOneCreatedStatus = 2003,
    ProductsListCannotBeChanged = 2004,
    ProductNameCannotBeChanged = 2005,
    ProductTypeCannotBeChanged = 2006,
    SupplierProductNumberCannotBeChanged = 2007
}
