using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Repositories;
using Jasmine.FastenerDepartment.Domain.Templates.Services;
using Jasmine.FastenerDepartment.EF.Repositories.UnitOfWork;

namespace Jasmine.FastenerDepartment.Application.Services.Templates;

internal class TemplatesService : ITemplatesService
{
    private readonly TemplateTypeCode[] _codesForExport = [TemplateTypeCode.ProductCatalog];

    private readonly ITemplatesRepository _templatesRepository;
    private readonly ITemplateTypesRepository _templatesTypesRepository;
    private readonly ITemplateContentTableColumnsRepository _templatesContentTableColumnsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TemplatesService(
        ITemplatesRepository templatesRepository,
        ITemplateTypesRepository templatesTypesRepository,
        ITemplateContentTableColumnsRepository templatesContentTableColumnsRepository,
        IUnitOfWork unitOfWork)
    {
        _templatesRepository = templatesRepository;
        _templatesTypesRepository = templatesTypesRepository;
        _templatesContentTableColumnsRepository = templatesContentTableColumnsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Template>> GetTemplatesAsync(CancellationToken cancellationToken = default)
    {
        var templates = await _templatesRepository.GetAllAsync(cancellationToken);
        return templates;
    }

    public async Task<IEnumerable<Template>> GetTemplatesForExportAsync(CancellationToken cancellationToken = default)
    {
        var templates = await _templatesRepository.GetByTypeCodesAsync(_codesForExport, cancellationToken);
        return templates;
    }

    public async Task<Template> GetTemplateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var template = await GetTemplateByIdAsync(id, cancellationToken);
        return template;
    }

    public async Task<IEnumerable<TemplateType>> GetTemplateTypesAsync(CancellationToken cancellationToken = default)
    {
        var templateTypes = await _templatesTypesRepository.GetAllAsync(cancellationToken);
        return templateTypes;
    }

    public IReadOnlyDictionary<TemplateTypeCode, IReadOnlyDictionary<Enum, LocalizedString>> GetContentTableColumns()
    {
        var columns = _templatesContentTableColumnsRepository.GetAll();
        return columns;
    }

    public IReadOnlyDictionary<Enum, LocalizedString> GetContentTableColumnsByTemplateTypeCode(TemplateTypeCode code)
    {
        var columns = _templatesContentTableColumnsRepository.GetByTemplateTypeCode(code);
        return columns;
    }

    public async Task<Template> CreateTemplateAsync(
        ChangeTemplate model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        var template = new Template(model.Name, model.TypeCode, model.Content);
        _templatesRepository.Add(template);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return template;
    }

    public async Task ChangeTemplateAsync(
        Guid id, ChangeTemplate model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));
        var template = await GetTemplateByIdAsync(id, cancellationToken);

        template.ChangeName(model.Name);
        template.ChangeType(model.TypeCode);
        template.ChangeContent(model.Content);

        _templatesRepository.Update(template);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
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
