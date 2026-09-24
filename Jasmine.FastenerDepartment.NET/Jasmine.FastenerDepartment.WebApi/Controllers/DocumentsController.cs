using Jasmine.FastenerDepartment.Domain.Templates.Services;
using Jasmine.FastenerDepartment.WebApi.Dtos.Templates;
using Jasmine.FastenerDepartment.WebApi.Dtos.Templates.RenderRequests;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.FastenerDepartment.WebApi.Controllers;

/// <summary>
/// Documents controller.
/// </summary>
[ApiController]
[Route("documents")]
public class DocumentsController : ControllerBase
{
    private readonly ITemplateRenderService _templateRenderService;
    private readonly WebApiMapper _mapper;

    /// <summary>
    /// Creates controller.
    /// </summary>
    /// <param name="templateRenderService">Template render service.</param>
    /// <param name="mapper">Mapper.</param>
    public DocumentsController(
        ITemplateRenderService templateRenderService,
        WebApiMapper mapper)
    {
        _templateRenderService = templateRenderService;
        _mapper = mapper;
    }

    /// <summary>
    /// Renders a preview of a document.
    /// </summary>
    /// <param name="dto">Create template model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Preview of a document.</returns>
    [HttpPost("preview")]
    public async Task<IActionResult> RenderTemplatePreviewAsync(
        [FromBody] ChangeTemplateDto dto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(dto);
        var render = await _templateRenderService.RenderPreviewAsync(model, cancellationToken);

        return File(render.Content, render.ContentType, render.FileName);
    }

    /// <summary>
    /// Renders an order form document.
    /// </summary>
    /// <param name="dto">Order form render request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Order form document.</returns>
    [HttpPost("order-form")]
    public async Task<IActionResult> RenderOrderFormTemplateAsync(
        [FromBody] OrderFormRenderRequestDto dto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(dto);
        var render = await _templateRenderService.RenderOrderFormAsync(model, cancellationToken);

        return File(render.Content, render.ContentType, render.FileName);
    }

    /// <summary>
    /// Render a product catalog document.
    /// </summary>
    /// <param name="dto">Product catalog render request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product catalog document.</returns>
    [HttpPost("product-catalog")]
    public async Task<IActionResult> RenderProductCatalogTemplateAsync(
        [FromBody] ProductCatalogRenderRequestDto dto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(dto);
        var render = await _templateRenderService.RenderProductCatalogAsync(model, cancellationToken);

        return File(render.Content, render.ContentType, render.FileName);
    }
}
