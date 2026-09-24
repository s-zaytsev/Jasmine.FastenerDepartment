using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.OrderForm;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.ProductCatalog;

namespace Jasmine.FastenerDepartment.Domain.Templates.Services;

/// <summary>
/// Template render service.
/// </summary>
public interface ITemplateRenderService
{
    /// <summary>
    /// Renders an order form document.
    /// </summary>
    /// <param name="model">Order form render request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Template render result.</returns>
    Task<TemplateRenderResult> RenderOrderFormAsync(
        OrderFormRenderRequest model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Renders a product catalog document.
    /// </summary>
    /// <param name="model">Product catalog render request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Template render result.</returns>
    Task<TemplateRenderResult> RenderProductCatalogAsync(
        ProductCatalogRenderRequest model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Renders preview of document.
    /// </summary>
    /// <param name="model">Change template model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Template render result.</returns>
    Task<TemplateRenderResult> RenderPreviewAsync(ChangeTemplate model, CancellationToken cancellationToken = default);
}
