using Jasmine.FastenerDepartment.Application.Constants;
using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Common.Services;
using Jasmine.FastenerDepartment.Domain.Templates.Factories;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.OrderForm;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.ProductCatalog;
using Jasmine.FastenerDepartment.Domain.Templates.Repositories;
using Jasmine.FastenerDepartment.Domain.Templates.Services;

namespace Jasmine.FastenerDepartment.Application.Services.Templates;

internal class TemplateRenderService : ITemplateRenderService
{
    private readonly ITemplatesRepository _templatesRepository;
    private readonly ITemplateFactory _templateFactory;
    private readonly ILanguageService _languageService;

    public TemplateRenderService(
        ITemplatesRepository templatesRepository,
        ITemplateFactory templateFactory,
        ILanguageService languageService)
    {
        _templatesRepository = templatesRepository;
        _templateFactory = templateFactory;
        _languageService = languageService;
    }

    public async Task<TemplateRenderResult> RenderOrderFormAsync(
        OrderFormRenderRequest model,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        var template = await GetTemplateByIdAsync(model.TemplateId);
        CheckTemplateType(template.TypeCode, TemplateTypeCode.OrderForm);

        var data = new OrderFormTemplateRenderData(
            model.Company,
            model.Order,
            template.Content,
            _languageService.LanguageCode);

        var render = Render(model.FormatCode, template.TypeCode, data);
        return render;
    }

    public async Task<TemplateRenderResult> RenderProductCatalogAsync(
        ProductCatalogRenderRequest model,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        var template = await GetTemplateByIdAsync(model.TemplateId);
        CheckTemplateType(template.TypeCode, TemplateTypeCode.ProductCatalog);

        var data = new ProductCatalogTemplateRenderData(
            template.Content, model.Products, _languageService.LanguageCode);

        var render = Render(model.FormatCode, template.TypeCode, data);
        return render;
    }

    public Task<TemplateRenderResult> RenderPreviewAsync(
        ChangeTemplate model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));


        TemplateRenderData data = model.TypeCode switch
        {
            TemplateTypeCode.ProductCatalog =>
                TemplatePreviewConstants.GetCatalogData(model.Content, _languageService.LanguageCode),

            TemplateTypeCode.OrderForm =>
                TemplatePreviewConstants.GetOrderReuqestData(model.Content, _languageService.LanguageCode),

            _ => throw new NotSupportedException($"Type {model.TypeCode} not supported."),
        };

        var render = Render(TemplateFormatCode.Html, model.TypeCode, data);
        return Task.FromResult(render);
    }

    private TemplateRenderResult Render(
        TemplateFormatCode formatCode,
        TemplateTypeCode typeCode,
        TemplateRenderData data)
    {
        var render = _templateFactory.Render(formatCode, typeCode, data);
        return render;
    }

    private async Task<Template> GetTemplateByIdAsync(Guid id)
    {
        var template = await _templatesRepository.GetByIdAsync(id);

        if (template == null)
        {
            throw new NotFoundException();
        }

        return template;
    }

    private void CheckTemplateType(TemplateTypeCode current, TemplateTypeCode expected)
    {
        if (current != expected) throw new InvalidOperationException("Types aren't equal.");
    }
}
