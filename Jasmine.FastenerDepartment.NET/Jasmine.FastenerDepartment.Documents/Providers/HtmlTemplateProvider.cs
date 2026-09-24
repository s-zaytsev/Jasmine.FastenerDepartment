using Jasmine.FastenerDepartment.Templates.Html.Builders;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content;
using Jasmine.FastenerDepartment.Domain.Templates.Providers;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.OrderForm;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.ProductCatalog;

namespace Jasmine.FastenerDepartment.Documents.Providers;

internal class HtmlTemplateProvider : ITemplateProvider
{
    public TemplateRenderResult Render(TemplateTypeCode code, TemplateRenderData data)
    {
        return code switch
        {
            TemplateTypeCode.OrderForm =>
                new OrderFormHtmlTemplateContentBuilder(data as OrderFormTemplateRenderData).Build(),
            TemplateTypeCode.ProductCatalog =>
                new ProductCatalogHtmlTemplateContentBuilder(data as ProductCatalogTemplateRenderData).Build(),
            _ => throw new NotSupportedException(),
        };
    }
}
