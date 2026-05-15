namespace Jasmine.FastenerDepartment.WebApi.Dtos.ProductTypes;

/// <summary>
/// Extended product type.
/// </summary>
/// <param name="Id">Identifier.</param>
/// <param name="Name">Name.</param>
/// <param name="ProductCount">Count of products.</param>
public record ExtendedProductTypeDto(Guid Id, string Name, int ProductCount);
