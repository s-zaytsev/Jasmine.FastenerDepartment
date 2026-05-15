using Jasmine.FastenerDepartment.WebApi.Dtos.Common;
using Jasmine.FastenerDepartment.WebApi.Dtos.ProductTypes;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Orders;

/// <summary>
/// Order product.
/// </summary>
/// <param name="Id">Identifier.</param>
/// <param name="ProductId">Product identifier.</param>
/// <param name="ProductName">Product name.</param>
/// <param name="Price">Product price.</param>
/// <param name="ProductType">Product type.</param>
/// <param name="Ordered">Ordered quantity.</param>
/// <param name="Fulfilled">Fulfilled quantity.</param>
/// <param name="SupplierProductNumber">Supplier product number.</param>
/// <param name="IsFulfilled">Is fulfilled.</param>
public record OrderProductDto(
    Guid Id,
    Guid? ProductId,
    string ProductName,
    decimal Price,
    ProductTypeDto ProductType,
    QuantityDto Ordered,
    QuantityDto Fulfilled,
    string SupplierProductNumber,
    bool IsFulfilled);
