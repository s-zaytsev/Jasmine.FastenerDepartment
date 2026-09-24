using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Services;
using Jasmine.FastenerDepartment.WebApi.Dtos.Templates;
using Jasmine.FastenerDepartment.WebApi.Dtos.Templates.Content;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.FastenerDepartment.WebApi.Controllers;

/// <summary>
/// Templates controller.
/// </summary>
[ApiController]
[Route("templates")]
public class TemplatesController : ControllerBase
{
    private readonly ITemplatesService _templatesService;
    private readonly WebApiMapper _mapper;

    /// <summary>
    /// Creates a controller.
    /// </summary>
    /// <param name="templatesService">Templates service.</param>
    /// <param name="mapper">Mapper.</param>
    public TemplatesController(
        ITemplatesService templatesService,
        WebApiMapper mapper)
    {
        _templatesService = templatesService;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns a collection of templates.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of templates.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TemplateDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTemplatesAsync(CancellationToken cancellationToken)
    {
        var templates = await _templatesService.GetTemplatesAsync(cancellationToken);
        var dtos = templates.Select(_mapper.Map);
        return Ok(dtos);
    }

    /// <summary>
    /// Returns a template.
    /// </summary>
    /// <param name="id">Template identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Template.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TemplateDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTemplateAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var template = await _templatesService.GetTemplateAsync(id, cancellationToken);
        var dto = _mapper.Map(template);
        return Ok(dto);
    }

    /// <summary>
    /// Returns a collection of template types.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of template types.</returns>
    [HttpGet("types")]
    [ProducesResponseType(typeof(IEnumerable<TemplateTypeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTemplateTypesAsync(CancellationToken cancellationToken)
    {
        var templateTypes = await _templatesService.GetTemplateTypesAsync(cancellationToken);
        var dtos = templateTypes.Select(_mapper.Map);
        return Ok(dtos);
    }

    /// <summary>
    /// Returns a collection of content table columns.
    /// </summary>
    /// <returns>Collection of content table columns.</returns>
    [HttpGet("content-table-columns")]
    [ProducesResponseType(typeof(IEnumerable<KeyValuePair<TemplateTypeCode, IEnumerable<TemplateContentTableColumnDto>>>),
        StatusCodes.Status200OK)]
    public IActionResult GetContentTableColumns()
    {
        var columns = _templatesService.GetContentTableColumns();
        var dtos = _mapper.Map(columns);

        return Ok(dtos);
    }

    /// <summary>
    /// Returns a collection of content table columns by template type code.
    /// </summary>
    /// <returns>Collection of content table columns.</returns>
    [HttpGet("content-table-columns/{code}")]
    [ProducesResponseType(typeof(IEnumerable<TemplateContentTableColumnDto>), StatusCodes.Status200OK)]
    public IActionResult GetContentTableColumnsByTemplateTypeCode([FromRoute] TemplateTypeCode code)
    {
        var columns = _templatesService.GetContentTableColumnsByTemplateTypeCode(code);
        var dtos = columns.Select(_mapper.Map);

        return Ok(dtos);
    }

    /// <summary>
    /// Creates a template.
    /// </summary>
    /// <param name="dto">Create template model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Template identifier.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateTemplateAsync(ChangeTemplateDto dto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(dto);
        var template = await _templatesService.CreateTemplateAsync(model, cancellationToken);
        return Ok(template.Id);
    }

    /// <summary>
    /// Creates a template.
    /// </summary>
    /// <param name="id">Template identifier.</param>
    /// <param name="dto">Create template model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ChangeTemplateAsync(
        [FromRoute] Guid id, [FromBody] ChangeTemplateDto dto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(dto);
        await _templatesService.ChangeTemplateAsync(id, model, cancellationToken);
        return NoContent();
    }
}
