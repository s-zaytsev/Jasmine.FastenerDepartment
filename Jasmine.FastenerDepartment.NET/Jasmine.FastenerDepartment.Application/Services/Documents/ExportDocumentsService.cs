using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Common.Services;
using Jasmine.FastenerDepartment.Domain.Documents.Models;
using Jasmine.FastenerDepartment.Domain.Documents.Services;
using Jasmine.FastenerDepartment.Domain.Messaging.Models;
using Jasmine.FastenerDepartment.Domain.Messaging.Services;
using Jasmine.FastenerDepartment.Domain.Products.Repositories;
using Jasmine.FastenerDepartment.Domain.Templates;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Repositories;
using Jasmine.FastenerDepartment.Domain.Templates.Services;

namespace Jasmine.FastenerDepartment.Application.Services.Documents;

internal class ExportDocumentsService : IExportDocumentsService
{
    private readonly TemplateTypeCode[] _codesForExport = [TemplateTypeCode.ProductCatalog];

    private readonly IReadOnlyDictionary<TemplateTypeCode, LocalizedString> _titles =
        new Dictionary<TemplateTypeCode, LocalizedString>
    {
        { TemplateTypeCode.ProductCatalog, new("Product catalog", "Каталог товаров") }
    };

    private readonly ITemplatesRepository _templatesRepository;
    private readonly ITemplateRenderService _templateRenderService;
    private readonly IProductsRepository _productsRepository;
    private readonly IMessagesService _messageService;
    private readonly ILanguageService _languageService;

    /// <summary>
    /// Creates a service.
    /// </summary>
    public ExportDocumentsService(
        ITemplatesRepository templatesRepository,
        ITemplateRenderService templateRenderService,
        IProductsRepository productsRepository,
        IMessagesService messageService,
        ILanguageService languageService)
    {
        _templatesRepository = templatesRepository;
        _templateRenderService = templateRenderService;
        _productsRepository = productsRepository;
        _messageService = messageService;
        _languageService = languageService;
    }

    public async Task<IEnumerable<Template>> GetTemplatesForExportAsync(
        CancellationToken cancellationToken = default)
    {
        var query = new TemplatesQuery { TypeCodes = _codesForExport };
        var templates = await _templatesRepository.GetAsync(query, cancellationToken);
        return templates;
    }

    public async Task<TemplateRenderResult> DownloadAsync(
        DownloadExportDocument model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(nameof(model));

        var template = await GetTemplateByIdAsync(model.TemplateId, cancellationToken);
        CheckTemplateTypeAsync(template);

        var (render, _) = await RenderExportDocumentAsync(
            model.TemplateId, model.FormatCode, template.TypeCode, cancellationToken);

        return render;
    }

    public async Task SendAsync(SendExportDocument model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(nameof(model));

        var template = await GetTemplateByIdAsync(model.TemplateId, cancellationToken);
        CheckTemplateTypeAsync(template);

        var (render, data) = await RenderExportDocumentAsync(
            model.TemplateId, model.FormatCode, template.TypeCode, cancellationToken);

        var messageRequest = new MessageRequest(
            model.MessageTypeCode,
            template.TypeCode,
            model.RecipientContact,
            _titles[template.TypeCode].GetText(_languageService.LanguageCode),
            data,
            [render.AsFile()]);

        await _messageService.SendAsync(messageRequest, cancellationToken);
    }

    private async Task<(TemplateRenderResult render, MessageRequestData data)> RenderExportDocumentAsync(
        Guid id,
        TemplateFormatCode formatCode,
        TemplateTypeCode code,
        CancellationToken cancellationToken)
    {
        var renderResult = code switch
        {
            TemplateTypeCode.ProductCatalog => await RenderProductCatalogAsync(id, formatCode, cancellationToken),
            _ => throw new NotSupportedException(),
        };

        return renderResult;
    }

    private async Task<(TemplateRenderResult, ProductCatalogMessageRequestData)> RenderProductCatalogAsync(
        Guid id, TemplateFormatCode formatCode, CancellationToken cancellationToken)
    {
        var products = await _productsRepository.GetAllAsync(cancellationToken);

        var renderResult =
            await _templateRenderService.RenderProductCatalogAsync(new(id, formatCode, products), cancellationToken);

        var data = new ProductCatalogMessageRequestData
        {
            Products = products,
            LanguageCode = _languageService.LanguageCode
        };

        return (renderResult, data);
    }

    private void CheckTemplateTypeAsync(Template template)
    {
        if (!_codesForExport.Contains(template.TypeCode))
            throw new NotSupportedException();
    }

    private async Task<Template> GetTemplateByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var template = await _templatesRepository.GetByIdAsync(id, cancellationToken);

        if (template == null)
        {
            throw new NotFoundException();
        }

        return template;
    }
}
