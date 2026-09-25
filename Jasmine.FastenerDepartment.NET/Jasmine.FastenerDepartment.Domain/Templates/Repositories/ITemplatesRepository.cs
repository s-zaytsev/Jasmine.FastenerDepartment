using Jasmine.FastenerDepartment.Domain.Common.Repositories;
using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.Domain.Templates.Repositories;

/// <summary>
/// Templates repository.
/// </summary>
public interface ITemplatesRepository : IRepository<Guid, Template>
{
    /// <summary>
    /// Returns a collection of templates by type codes.
    /// </summary>
    /// <param name="codes">Template type codes.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of templates by type codes.</returns>
    Task<ICollection<Template>> GetByTypeCodesAsync(
        TemplateTypeCode[] codes, CancellationToken cancellationToken = default);
}
