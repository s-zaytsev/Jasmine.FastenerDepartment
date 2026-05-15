namespace Jasmine.FastenerDepartment.WebApi.Dtos.ProductsToOrder;

/// <summary>
/// Supplier number.
/// </summary>
/// <param name="SupplierId">Supplier identifier.</param>
/// <param name="Number">Number.</param>
public record SupplierNumberDto(Guid SupplierId, string Number);
