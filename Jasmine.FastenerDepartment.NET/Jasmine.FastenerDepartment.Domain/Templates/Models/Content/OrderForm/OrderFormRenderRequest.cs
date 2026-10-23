using Jasmine.FastenerDepartment.Domain.Companies.Models;
using Jasmine.FastenerDepartment.Domain.Orders.Models;

namespace Jasmine.FastenerDepartment.Domain.Templates.Models.Content.OrderForm;

/// <summary>
/// Order form render request.
/// </summary>
public class OrderFormRenderRequest : RenderRequestBase
{
    /// <summary>
    /// Order.
    /// </summary>
    public Order Order { get; init; }

    /// <summary>
    /// Company.
    /// </summary>
    public Company Company { get; init; }

    /// <summary>
    /// Creates an order form render request.
    /// </summary>
    /// <param name="templateId">Template identifier.</param>
    /// <param name="formatCode">Format code.</param>
    /// <param name="order">Order.</param>
    /// <param name="company">Company.</param>
    public OrderFormRenderRequest(
        Guid templateId,
        TemplateFormatCode formatCode,
        Order order,
        Company company)
        : base(templateId, formatCode)
    {
        Order = order;
        Company = company;
    }
}
