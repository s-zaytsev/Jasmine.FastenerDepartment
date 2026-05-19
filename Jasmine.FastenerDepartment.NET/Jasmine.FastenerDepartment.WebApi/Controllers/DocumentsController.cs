using Jasmine.FastenerDepartment.Application.Services.Documents;
using Jasmine.FastenerDepartment.WebApi.Dtos.Documents;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.FastenerDepartment.WebApi.Controllers;

/// <summary>
/// Documents controller.
/// </summary>
[ApiController]
[Route("documents")]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentsService _documentsService;
    private readonly WebApiMapper _mapper;

    /// <summary>
    /// Creates controller.
    /// </summary>
    /// <param name="documentsService">Document service.</param>
    /// <param name="mapper">Mapper.</param>
    public DocumentsController(
        IDocumentsService documentsService,
        WebApiMapper mapper)
    {
        _documentsService = documentsService;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns the file.
    /// </summary>
    /// <param name="model">Export document request</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>File.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(File), StatusCodes.Status200OK)]
    public async Task<IActionResult> DownloadDocumentAsync(
        [FromQuery] ExportDocumentRequestDto model, CancellationToken cancellationToken)
    {
        var request = _mapper.Map(model);
        var response = await _documentsService.ExportDocumentAsync(request, cancellationToken);

        return File(response.Stream, "application/octet-stream", response.Name);
    }
}
