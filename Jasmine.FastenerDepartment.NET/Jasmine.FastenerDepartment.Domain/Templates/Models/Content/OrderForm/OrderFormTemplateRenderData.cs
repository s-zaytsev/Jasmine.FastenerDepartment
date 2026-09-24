using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Companies.Models;
using Jasmine.FastenerDepartment.Domain.Orders.Models;

namespace Jasmine.FastenerDepartment.Domain.Templates.Models.Content.OrderForm;

/// <summary>
/// Order form template render data.
/// </summary>
public class OrderFormTemplateRenderData : TemplateRenderData
{
    /// <summary>
    /// Company.
    /// </summary>
    public Company Company { get; init; }

    /// <summary>
    /// Order.
    /// </summary>
    public Order Order { get; init; }

    /// <summary>
    /// Order request template content.
    /// </summary>
    public OrderFormTemplateContent Content { get; init; }

    /// <summary>
    /// Create an order template render data.
    /// </summary>
    /// <param name="company">Company.</param>
    /// <param name="order">Order.</param>
    /// <param name="content">Order request template content.</param>
    /// <param name="languageCode">Language code.</param>
    public OrderFormTemplateRenderData(
        Company company,
        Order order,
        TemplateContent content,
        LanguageCode? languageCode)
        : base(languageCode)
    {
        Company = company;
        Order = order;
        Content = content as OrderFormTemplateContent;
    }
}
