using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Common.Exceptions.Codes;
using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Models;

namespace Jasmine.FastenerDepartment.Domain.Orders.Models;

/// <summary>
/// Order.
/// </summary>
public class Order : AggregateRootBase<Guid>
{
    private List<OrderProduct> _products = [];
    private readonly List<OrderHistoryEntry> _historyEntries = [];

    /// <summary>
    /// Number.
    /// </summary>
    public int Number { get; init; }

    /// <summary>
    /// Supplier identifier.
    /// </summary>
    public Guid? SupplierId { get; init; }

    /// <summary>
    /// Date when order has been closed.
    /// </summary>
    public DateTime? ClosedDate { get; private set; }

    /// <summary>
    /// Status code.
    /// </summary>
    public OrderStatusCode StatusCode { get; private set; }

    /// <summary>
    /// Status.
    /// </summary>
    public OrderStatus Status { get; private set; }

    /// <summary>
    /// Supplier.
    /// </summary>
    public Supplier Supplier { get; private set; }

    /// <summary>
    /// List of products.
    /// </summary>
    public IReadOnlyCollection<OrderProduct> Products => _products.AsReadOnly();

    /// <summary>
    /// List of history entries.
    /// </summary>
    public IReadOnlyCollection<OrderHistoryEntry> HistoryEntries => _historyEntries.AsReadOnly();

    private Order() { }

    /// <summary>
    /// Creates the order.
    /// </summary>
    /// <param name="number">Number.</param>
    /// <param name="supplierId">Supplier identifier.</param>
    /// <param name="products">Order products.</param>
    public Order(
        int number,
        Guid? supplierId,
        ICollection<OrderProduct> products)
    {
        if (products?.Count == 0)
        {
            DomainGuard.ThrowOrderException(OrderExceptionCode.EmptyProductList);
        }

        Number = number;
        SupplierId = supplierId;
        _products = [.. products];
        ChangeStatus(OrderStatusCode.Created);
    }

    /// <summary>
    /// Changes order status.
    /// </summary>
    /// <param name="statusCode">Order status code.</param>
    /// <param name="comment">Comment.</param>
    public void ChangeStatus(OrderStatusCode statusCode, string comment = null)
    {
        if (StatusCode == OrderStatusCode.Fulfilled || StatusCode == OrderStatusCode.Cancelled)
        {
            DomainGuard.ThrowOrderException(OrderExceptionCode.ChangingStatusNotAllowed);
        }

        if (statusCode == OrderStatusCode.Created && HistoryEntries.Any(x => x.OrderStatusCode == OrderStatusCode.Created))
        {
            DomainGuard.ThrowOrderException(OrderExceptionCode.OnlyOneCreatedStatus);
        }

        StatusCode = statusCode;

        var historyEntry = new OrderHistoryEntry(DateTime.UtcNow, statusCode, comment);
        _historyEntries.Add(historyEntry);
    }

    /// <summary>
    /// Changes the products.
    /// </summary>
    /// <param name="products">List of products.</param>
    public void ChangeProducts(ICollection<OrderProduct> products)
    {
        if (StatusCode == OrderStatusCode.Fulfilled || StatusCode == OrderStatusCode.Cancelled)
        {
            DomainGuard.ThrowOrderException(OrderExceptionCode.ProductsListCannotBeChanged);
        }

        if (products == null || products.Count == 0) 
        {
            DomainGuard.ThrowOrderException(OrderExceptionCode.EmptyProductList);
        }

        _products = [.. products];
    }
}
