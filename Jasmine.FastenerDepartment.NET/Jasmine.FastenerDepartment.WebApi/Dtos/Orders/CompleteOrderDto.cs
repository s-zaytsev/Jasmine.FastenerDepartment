namespace Jasmine.FastenerDepartment.WebApi.Dtos.Orders;

/// <summary>
/// Complete order.
/// </summary>
/// <param name="Comment">Comment.</param>
/// <param name="Products">Products.</param>
public record CompleteOrderDto(
    string Comment,
    ICollection<CompleteOrderProductDto> Products);
