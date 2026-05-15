using Jasmine.FastenerDepartment.Domain.Common.Repositories;
using Jasmine.FastenerDepartment.Domain.ProductTypes.Models;

namespace Jasmine.FastenerDepartment.Domain.ProductTypes.Repositories;

/// <summary>
/// Product types repository.
/// </summary>
public interface IProductTypesRepository : IRepository<Guid, ProductType>
{
    /// <summary>
    /// Returns a collection of extended product types.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of extended product types.</returns>
    Task<IEnumerable<ExtendedProductType>> GetAllExtendedProductTypesAsync(
        CancellationToken cancellationToken = default);
}
