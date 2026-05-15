namespace Jasmine.FastenerDepartment.WebApi.Dtos.Orders;

/// <summary>
/// Create order model.
/// </summary>
/// <param name="SupplierId">Supplier identifier.</param>
/// <param name="Products">Products.</param>
public record CreateOrderDto(
    Guid? SupplierId,
    ICollection<ChangeOrderProductDto> Products);
