using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Suppliers;

/// <summary>
/// Supplier products query.
/// </summary>
public class SupplierProductsQueryDto : QueryBase<SupplierProductsQueryParameter>
{
    /// <summary>
    /// Supplier identifier.
    /// </summary>
    public Guid SupplierId { get; set; }
}
