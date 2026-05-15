using Jasmine.FastenerDepartment.WebApi.Dtos.Products;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.ProductsToOrder;

/// <summary>
/// Product to order.
/// </summary>
/// <param name="Product">Product.</param>
/// <param name="SupplierNumbers">Numbers of product from suppliers' databases.</param>
public record ProductToOrderDto(ProductDto Product, ICollection<SupplierNumberDto> SupplierNumbers);
