using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.OrderForm;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.ProductCatalog;
using Jasmine.FastenerDepartment.Domain.Templates.Providers;
using Jasmine.FastenerDepartment.Templates.Pdf.Builders;

namespace Jasmine.FastenerDepartment.Documents.Providers;

internal class PdfTemplateProvider : ITemplateProvider
{
    public TemplateRenderResult Render(TemplateTypeCode code, TemplateRenderData data)
    {
        return code switch
        {
            TemplateTypeCode.OrderForm =>
                new OrderFormPdfTemplateContentBuilder(data as OrderFormTemplateRenderData).Build(),
            TemplateTypeCode.ProductCatalog =>
                new ProductCatalogPdfTemplateContentBuilder(data as ProductCatalogTemplateRenderData).Build(),
            _ => throw new NotSupportedException(),
        };
    }
}
