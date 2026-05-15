namespace Jasmine.FastenerDepartment.WebApi.Dtos.Suppliers;

/// <summary>
/// Supplier.
/// </summary>
/// <param name="Id">Identifier.</param>
/// <param name="Name">Name.</param>
/// <param name="Address">Address.</param>
/// <param name="ProductCount">Count of products.</param>
/// <param name="ActiveOrderCount">Count of active orders.</param>
public record ExtendedSupplierDto(
    Guid Id,
    string Name,
    string Address,
    int ProductCount,
    int ActiveOrderCount);
