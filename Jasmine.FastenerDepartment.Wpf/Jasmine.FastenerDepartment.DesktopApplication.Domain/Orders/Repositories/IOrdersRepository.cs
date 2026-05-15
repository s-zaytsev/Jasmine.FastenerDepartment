using Jasmine.FastenerDepartment.Domain.Common.Repositories;
using Jasmine.FastenerDepartment.Domain.Orders.Models;

namespace Jasmine.FastenerDepartment.Domain.Orders.Repositories;

/// <summary>
/// Orders repository.
/// </summary>
public interface IOrdersRepository : IRepository<Guid, Order>
{
    /// <summary>
    /// Returns the last order number.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The last order number.</returns>
    Task<int> GetLastNumberAsync(CancellationToken cancellationToken = default);
}
