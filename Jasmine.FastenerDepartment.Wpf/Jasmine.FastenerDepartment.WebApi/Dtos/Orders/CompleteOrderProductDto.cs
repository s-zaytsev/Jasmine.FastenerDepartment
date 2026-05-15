using Jasmine.FastenerDepartment.WebApi.Dtos.Common;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Orders;

/// <summary>
/// Complete order product.
/// </summary>
/// <param name="OrderProductId">Order product identifier.</param>
/// <param name="ProductId">Product identifier.</param>
/// <param name="ProductName">Product name.</param>
/// <param name="Price">Price.</param>
/// <param name="ProductTypeId">Product type identifier.</param>
/// <param name="IsFulfilled">Is fulfilled.</param>
/// <param name="AddToPrint">Should products from order be added to print list?</param>
/// <param name="RemoveOrderStatus">Should the order status be removed from the products in the order?</param>
/// <param name="Fulfilled">Fulfilled quantity.</param>
/// <param name="SupplierProductNumber">Product number from supplier database.</param>
public record CompleteOrderProductDto(
    Guid? OrderProductId,
    Guid? ProductId,
    string ProductName,
    decimal Price,
    Guid? ProductTypeId,
    bool IsFulfilled,
    bool AddToPrint,
    bool RemoveOrderStatus,
    QuantityDto Fulfilled,
    string SupplierProductNumber);