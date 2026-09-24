using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.OrderForm;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.ProductCatalog;
using Jasmine.FastenerDepartment.Domain.Templates.Providers;
using Jasmine.FastenerDepartment.Templates.OfficeDocuments.Builders;

namespace Jasmine.FastenerDepartment.Documents.Providers;

internal class WordTemplateProvider : ITemplateProvider
{
    public TemplateRenderResult Render(TemplateTypeCode code, TemplateRenderData data)
    {
        return code switch
        {
            TemplateTypeCode.OrderForm =>
                new OrderFormWordTemplateContentBuilder(data as OrderFormTemplateRenderData).Build(),
            TemplateTypeCode.ProductCatalog =>
                new ProductCatalogWordTemplateContentBuilder(data as ProductCatalogTemplateRenderData).Build(),
            _ => throw new NotSupportedException(),
        };
    }
}
