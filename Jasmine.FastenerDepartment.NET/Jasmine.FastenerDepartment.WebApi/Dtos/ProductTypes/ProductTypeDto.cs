namespace Jasmine.FastenerDepartment.WebApi.Dtos.ProductTypes;

/// <summary>
/// Product type.
/// </summary>
/// <param name="Id">Identifier.</param>
/// <param name="Name">Name.</param>
public record ProductTypeDto(Guid Id, string Name);