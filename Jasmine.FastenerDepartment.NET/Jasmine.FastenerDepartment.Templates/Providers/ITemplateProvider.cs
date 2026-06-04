using Jasmine.FastenerDepartment.Domain.Orders.Models;

namespace Jasmine.FastenerDepartment.Templates.Providers;

internal interface ITemplateProvider
{
    string GetOrderRequestTemplate(Order order, bool hasAttachments);
}
