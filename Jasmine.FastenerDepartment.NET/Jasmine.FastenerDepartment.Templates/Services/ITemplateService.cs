using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Jasmine.FastenerDepartment.Templates.Models;

namespace Jasmine.FastenerDepartment.Templates.Services;

/// <summary>
/// Template service.
/// </summary>
public interface ITemplateService
{
    /// <summary>
    /// Returns a order request template.
    /// </summary>
    /// <param name="type">Template type.</param>
    /// <param name="order">Order.</param>
    /// <returns>Order request template.</returns>
    string GetOrderRequestTemplate(TemplateType type, Order order);
}
