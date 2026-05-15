namespace Jasmine.FastenerDepartment.WebApi.Dtos.Suppliers;

/// <summary>
/// Supplier.
/// </summary>
/// <param name="Id">Identifier.</param>
/// <param name="Name">Name.</param>
/// <param name="Address">Address.</param>
public record SupplierDto(Guid Id, string Name, string Address);
