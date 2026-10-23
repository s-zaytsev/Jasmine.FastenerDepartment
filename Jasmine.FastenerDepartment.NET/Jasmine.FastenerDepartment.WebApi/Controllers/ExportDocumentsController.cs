using Jasmine.FastenerDepartment.Domain.Documents.Services;
using Jasmine.FastenerDepartment.WebApi.Dtos.Documents;
using Jasmine.FastenerDepartment.WebApi.Dtos.Templates;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.FastenerDepartment.WebApi.Controllers;

/// <summary>
/// Export documents controller.
/// </summary>
[ApiController]
[Route("export-documents")]
public class ExportDocumentsController : ControllerBase
{
    private readonly IExportDocumentsService _exportDocumentsService;
    private readonly WebApiMapper _mapper;

    /// <summary>
    /// Creates controller.
    /// </summary>
    /// <param name="exportDocumentsService">Export documents service.</param>
    /// <param name="mapper">Mapper.</param>
    public ExportDocumentsController(
        IExportDocumentsService exportDocumentsService,
        WebApiMapper mapper)
    {
        _exportDocumentsService = exportDocumentsService;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns a collection of templates for export.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of templates for export.</returns>
    [HttpGet("export-documents")]
    [ProducesResponseType(typeof(IEnumerable<TemplateDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTemplatesForExportAsync(CancellationToken cancellationToken)
    {
        var templates = await _exportDocumentsService.GetTemplatesForExportAsync(cancellationToken);
        var dtos = templates.Select(_mapper.Map);
        return Ok(dtos);
    }

    /// <summary>
    /// Returns an export document.
    /// </summary>
    /// <param name="dto">Download export document model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Document.</returns>
    [HttpPost("download")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DownloadDocumentAsync(
        [FromBody] DownloadExportDocumentDto dto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(dto);
        var render = await _exportDocumentsService.DownloadAsync(model, cancellationToken);

        return File(render.Content, render.ContentType, render.FileName);
    }

    /// <summary>
    /// Sends an export document.
    /// </summary>
    /// <param name="dto">Send export document model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPost("send")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> SendDocumentAsync(
        [FromBody] SendExportDocumentDto dto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(dto);
        await _exportDocumentsService.SendAsync(model, cancellationToken);

        return NoContent();
    }
}
