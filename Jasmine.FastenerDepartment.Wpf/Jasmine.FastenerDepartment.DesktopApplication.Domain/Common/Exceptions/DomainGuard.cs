using Jasmine.FastenerDepartment.Domain.Common.Exceptions.Codes;
using Jasmine.FastenerDepartment.Domain.Common.Exceptions.Messages;
using Jasmine.FastenerDepartment.Domain.Common.Extensions;

namespace Jasmine.FastenerDepartment.Domain.Common.Exceptions;

internal static class DomainGuard
{
    private static readonly IDictionary<Enum, string> _commonExceptions = new Dictionary<Enum, string>
    {
        { CommonExceptionCode.IncorrectName, CommonExceptionMessages.IncorrectName },
        { CommonExceptionCode.IncorrectPrice, CommonExceptionMessages.IncorrectPrice },
        { CommonExceptionCode.IncorrectQuantity, CommonExceptionMessages.IncorrectQuantity },
        { CommonExceptionCode.SpecialMeasurementNotAllowed, CommonExceptionMessages.SpecialMeasurementNotAllowed },
        { CommonExceptionCode.MeasurementUnitNotFound, CommonExceptionMessages.MeasurementUnitNotFound }
    };

    private static readonly IDictionary<Enum, string> _orderExceptions = new Dictionary<Enum, string>
    {
        { OrderExceptionCode.EmptyProductList, OrderExceptionMessages.EmptyProductList },
        { OrderExceptionCode.ChangingStatusNotAllowed, OrderExceptionMessages.ChangingStatusNotAllowed },
        { OrderExceptionCode.OnlyOneCreatedStatus, OrderExceptionMessages.OnlyOneCreatedStatus },
        { OrderExceptionCode.ProductsListCannotBeChanged, OrderExceptionMessages.ProductsListCannotBeChanged },
        { OrderExceptionCode.ProductNameCannotBeChanged, OrderExceptionMessages.ProductNameCannotBeChanged },
        { OrderExceptionCode.ProductTypeCannotBeChanged, OrderExceptionMessages.ProductTypeCannotBeChanged },
        { OrderExceptionCode.SupplierProductNumberCannotBeChanged, OrderExceptionMessages.SupplierProductNumberCannotBeChanged }
    };

    private static readonly IDictionary<Enum, string> _productExceptions = new Dictionary<Enum, string>
    {
        { ProductExceptionCode.IncorrectNumber, ProductExceptionMessages.IncorrectNumber }
    };

    public static void ThrowCommonException(CommonExceptionCode code, string serverMessage = null)
    {
        var userMessage = _commonExceptions[code];
        throw new DomainException(code.Ordinal(), serverMessage ?? userMessage, userMessage);
    }

    public static void ThrowOrderException(OrderExceptionCode code, string serverMessage = null)
    {
        var userMessage = _orderExceptions[code];
        throw new DomainException(code.Ordinal(), serverMessage ?? userMessage, userMessage);
    }

    public static void ThrowProductException(ProductExceptionCode code, string serverMessage = null)
    {
        var userMessage = _productExceptions[code];
        throw new DomainException(code.Ordinal(), serverMessage ?? userMessage, userMessage);
    }
}
