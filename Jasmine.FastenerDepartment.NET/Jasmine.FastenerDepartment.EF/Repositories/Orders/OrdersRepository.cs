using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Jasmine.FastenerDepartment.Domain.Orders.Repositories;
using Jasmine.FastenerDepartment.EF.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jasmine.FastenerDepartment.EF.Repositories.Orders;

internal class OrdersRepository : RepositoryBase<Guid, Order>, IOrdersRepository
{
    public OrdersRepository(ApplicationDbContext context)
        : base(context)
    { }

    public async override Task<Order> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetQuery()
            .Include(x => x.Supplier)
            .Include(x => x.Products).ThenInclude(x => x.Product).ThenInclude(x => x.Type)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<int> GetLastNumberAsync(CancellationToken cancellationToken)
    {
        var order = await GetQuery().OrderByDescending(x => x.Number).FirstOrDefaultAsync(cancellationToken);
        return order?.Number ?? 0;
    }

    protected override IQueryable<Order> GetQuery()
    {
        return base.GetQuery()
            .Include(x => x.Supplier)
            .Include(x => x.Products)
            .Include(x => x.HistoryEntries);
    }

    protected override IEnumerable<Expression<Func<Order, bool>>> GetFilterings<T, K>(
        IQueryable<Order> dbQuery, T query)
    {
        var ordersQuery = query as OrdersQuery;

        var predicates = new List<Expression<Func<Order, bool>>>();

        if (ordersQuery.OnlyCompleted)
        {
            predicates.Add(x =>
                x.StatusCode == OrderStatusCode.Fulfilled ||
                x.StatusCode == OrderStatusCode.Cancelled);
        }

        if (ordersQuery.OnlyActive)
        {
            predicates.Add(x =>
                x.StatusCode == OrderStatusCode.Created ||
                x.StatusCode == OrderStatusCode.Sent);
        }

        return predicates;
    }
}
