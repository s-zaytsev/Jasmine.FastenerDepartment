using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Suppliers.Models;

/// <summary>
/// Supplier products query.
/// </summary>
public class SupplierProductsQuery : QueryBase<SupplierProductsQueryParameter>
{
    /// <summary>
    /// Supplier identifier.
    /// </summary>
    public Guid SupplierId { get; set; }
}
