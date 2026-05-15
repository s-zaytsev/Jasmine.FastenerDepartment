namespace Jasmine.FastenerDepartment.WebApi.Dtos.ProductsToOrder;

/// <summary>
/// Products to order query.
/// </summary>
/// <param name="Search">Search.</param>
/// <param name="SupplierId">Supplier identifier.</param>
/// <param name="OnlyToOrder">Select only products with IsNeededToOrder flag.</param>
public record ProductsToOrderQueryDto(string Search, Guid? SupplierId, bool OnlyToOrder);
