using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.Domain.Templates.Services;

/// <summary>
/// Templates service.
/// </summary>
public interface ITemplatesService
{
    /// <summary>
    /// Returns a collection of templates.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of templates.</returns>
    Task<IEnumerable<Template>> GetTemplatesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a template.
    /// </summary>
    /// <param name="id">Template identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Template.</returns>
    Task<Template> GetTemplateAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a collection of template types.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of template types.</returns>
    Task<IEnumerable<TemplateType>> GetTemplateTypesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a collection of content table columns.
    /// </summary>
    /// <returns>Collection of content table columns.</returns>
    IReadOnlyDictionary<TemplateTypeCode, IReadOnlyDictionary<Enum, LocalizedString>> GetContentTableColumns();

    /// <summary>
    /// Returns a collection of content table columns by template type code.
    /// </summary>
    /// <returns>Collection of content table columns.</returns>
    IReadOnlyDictionary<Enum, LocalizedString> GetContentTableColumnsByTemplateTypeCode(TemplateTypeCode code);

    /// <summary>
    /// Creates a template.
    /// </summary>
    /// <param name="model">Change template model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Template.</returns>
    Task<Template> CreateTemplateAsync(ChangeTemplate model,  CancellationToken cancellationToken = default);

    /// <summary>
    /// Changes a template.
    /// </summary>
    /// <param name="id">Template identifier.</param>
    /// <param name="model">Change template model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task ChangeTemplateAsync(Guid id, ChangeTemplate model, CancellationToken cancellationToken = default);
}
