namespace Jasmine.FastenerDepartment.WebApi.Dtos.Orders;

/// <summary>
/// Change order.
/// </summary>
/// <param name="Products">Products.</param>
public record ChangeOrderDto(ICollection<ChangeOrderProductDto> Products);
