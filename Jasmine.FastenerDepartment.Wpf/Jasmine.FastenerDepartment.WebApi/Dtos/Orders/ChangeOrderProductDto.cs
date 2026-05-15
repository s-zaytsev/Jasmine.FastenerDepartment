using Jasmine.FastenerDepartment.WebApi.Dtos.Common;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Orders;

/// <summary>
/// Change order product.
/// </summary>
/// <param name="ProductId">Product identifier.</param>
/// <param name="ProductName">Product name.</param>
/// <param name="ProductTypeId">Product type identifier.</param>
/// <param name="Ordered">Ordered quantity.</param>
/// <param name="SupplierProductNumber">Supplier product number.</param>
public record ChangeOrderProductDto(
    Guid? ProductId,
    string ProductName,
    Guid? ProductTypeId,
    QuantityDto Ordered,
    string SupplierProductNumber);
