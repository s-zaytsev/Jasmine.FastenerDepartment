using Jasmine.FastenerDepartment.Domain.Messaging.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Templates.Emails.Builders;

namespace Jasmine.FastenerDepartment.Templates.Emails.Factories;

internal class EmailTemplatesFactory : IEmailTemplatesFactory
{
    public string Render(TemplateTypeCode code, MessageRequestData data)
    {

        if (code == TemplateTypeCode.OrderForm && data is OrderFormMessageRequestData orderFormData)
        {
            return new OrderFormEmailTemplateBuilder(
                orderFormData.Order,
                orderFormData.HasAttachments,
                orderFormData.LanguageCode)
                .Build();
        }
        else if (code == TemplateTypeCode.ProductCatalog && data is ProductCatalogMessageRequestData catalogData)
        {
            return new ProductCatalogEmailTemplateBuilder(catalogData.Products, catalogData.LanguageCode)
                .Build();
        }
        else
        {
            throw new NotSupportedException($"Code {code} not supported.");
        }
    }
}
