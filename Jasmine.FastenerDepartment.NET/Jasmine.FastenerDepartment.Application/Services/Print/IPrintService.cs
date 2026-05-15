using Jasmine.FastenerDepartment.Domain.Products.Models;

namespace Jasmine.FastenerDepartment.Application.Services.Print;

/// <summary>
/// Print service.
/// </summary>
public interface IPrintService
{
    /// <summary>
    /// Returns the list of products to print.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of products to print.</returns>
    Task<IEnumerable<Product>> GetProductsToPrintAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes the product from the print list.
    /// </summary>
    /// <param name="id">Product identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task RemoveProductFromPrintListAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes all products from the print list. 
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task RemoveAllProductsFromPrintListAsync(CancellationToken cancellationToken = default);
}
