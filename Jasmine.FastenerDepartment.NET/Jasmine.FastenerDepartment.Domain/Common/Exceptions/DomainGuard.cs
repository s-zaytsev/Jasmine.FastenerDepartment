using Jasmine.FastenerDepartment.Domain.Common.Exceptions.Codes;
using Jasmine.FastenerDepartment.Domain.Common.Exceptions.Messages;
using Jasmine.FastenerDepartment.Domain.Common.Extensions;
using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Common.Exceptions;

internal static class DomainGuard
{
    private static readonly IDictionary<Enum, LocalizedString> _commonExceptions = new Dictionary<Enum, LocalizedString>
    {
        { CommonExceptionCode.IncorrectName, CommonExceptionMessages.IncorrectName },
        { CommonExceptionCode.IncorrectPrice, CommonExceptionMessages.IncorrectPrice },
        { CommonExceptionCode.IncorrectQuantity, CommonExceptionMessages.IncorrectQuantity },
        { CommonExceptionCode.SpecialMeasurementNotAllowed, CommonExceptionMessages.SpecialMeasurementNotAllowed },
        { CommonExceptionCode.MeasurementUnitNotFound, CommonExceptionMessages.MeasurementUnitNotFound }
    };

    private static readonly IDictionary<Enum, LocalizedString> _orderExceptions = new Dictionary<Enum, LocalizedString>
    {
        { OrderExceptionCode.EmptyProductList, OrderExceptionMessages.EmptyProductList },
        { OrderExceptionCode.ChangingStatusNotAllowed, OrderExceptionMessages.ChangingStatusNotAllowed },
        { OrderExceptionCode.OnlyOneCreatedStatus, OrderExceptionMessages.OnlyOneCreatedStatus },
        { OrderExceptionCode.ProductsListCannotBeChanged, OrderExceptionMessages.ProductsListCannotBeChanged },
        { OrderExceptionCode.ProductNameCannotBeChanged, OrderExceptionMessages.ProductNameCannotBeChanged },
        { OrderExceptionCode.ProductTypeCannotBeChanged, OrderExceptionMessages.ProductTypeCannotBeChanged },
        { OrderExceptionCode.SupplierProductNumberCannotBeChanged, OrderExceptionMessages.SupplierProductNumberCannotBeChanged }
    };

    private static readonly IDictionary<Enum, LocalizedString> _productExceptions = new Dictionary<Enum, LocalizedString>
    {
        { ProductExceptionCode.IncorrectNumber, ProductExceptionMessages.IncorrectNumber }
    };

    public static void ThrowCommonException(CommonExceptionCode code)
    {
        var localizedText = _commonExceptions[code];
        Throw(code, localizedText);
    }

    public static void ThrowOrderException(OrderExceptionCode code)
    {
        var localizedText = _orderExceptions[code];
        Throw(code, localizedText);
    }

    public static void ThrowProductException(ProductExceptionCode code)
    {
        var localizedText = _productExceptions[code];
        Throw(code, localizedText);
    }

    private static void Throw<T>(T code, LocalizedString localizedText)
        where T : Enum
    {
        throw new DomainException(code.Ordinal(), localizedText.En, localizedText);
    }
}
