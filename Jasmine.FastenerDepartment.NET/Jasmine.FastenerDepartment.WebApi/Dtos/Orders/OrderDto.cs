using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Jasmine.FastenerDepartment.WebApi.Dtos.HistoryEntries;
using Jasmine.FastenerDepartment.WebApi.Dtos.Suppliers;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Orders;

/// <summary>
/// Order.
/// </summary>
/// <param name="Id">Identifier.</param>
/// <param name="CreatedDate">Created date.</param>
/// <param name="Number">Number.</param>
/// <param name="StatusCode">Status code.</param>
/// <param name="Supplier">Supplier.</param>
/// <param name="Products">Products.</param>
/// <param name="HistoryEntries">History entries.</param>
public record OrderDto(
    Guid Id,
    DateTime CreatedDate,
    string Number,
    OrderStatusCode StatusCode,
    SupplierDto Supplier,
    ICollection<OrderProductDto> Products,
    ICollection<OrderHistoryEntryDto> HistoryEntries);
