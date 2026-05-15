using Jasmine.FastenerDepartment.Domain.ProductTypes.Models;

namespace Jasmine.FastenerDepartment.Domain.ProductTypes.Services;

/// <summary>
/// Product types service.
/// </summary>
public interface IProductTypesService
{
    /// <summary>
    /// Returns the collection of product types.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of product types.</returns>
    Task<IEnumerable<ProductType>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a collection of extended product types.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of extended product types.</returns>
    Task<IEnumerable<ExtendedProductType>> GetAllExtendedProductTypesAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new product type.
    /// </summary>
    /// <param name="model">Change product type model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product type.</returns>
    Task<ProductType> CreateAsync(ChangeProductType model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the product type.
    /// </summary>
    /// <param name="id">Product type identifier..</param>
    /// <param name="model">Change product type model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task UpdateAsync(Guid id, ChangeProductType model, CancellationToken cancellationToken = default);
}
