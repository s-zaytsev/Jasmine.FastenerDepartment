using Jasmine.FastenerDepartment.WebApi.Dtos.Products;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Suppliers;

/// <summary>
/// Supplier product.
/// </summary>
/// <param name="Id">Identifier.</param>
/// <param name="Number">Number.</param>
/// <param name="Product">Product.</param>
public record SupplierProductDto(Guid Id, string Number, ProductDto Product);
