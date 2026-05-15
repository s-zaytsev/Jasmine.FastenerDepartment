using Jasmine.FastenerDepartment.Application.Services.Synchronization;
using Jasmine.FastenerDepartment.WebApi.Dtos.Synchronization;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.FastenerDepartment.WebApi.Controllers;

/// <summary>
/// Synchronization controller.
/// </summary>
[ApiController]
[Route("synchronization")]
public class SynchronizationController : ControllerBase
{
    private readonly ISynchronizationService _service;

    /// <summary>
    /// Creates controller.
    /// </summary>
    /// <param name="service">Synchronization service.</param>
    public SynchronizationController(ISynchronizationService service)
    {
        _service = service;
    }

    /// <summary>
    /// Returns the synchronization response.
    /// </summary>
    /// <param name="model">Synchronization request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Synchronization response.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(SynchronizationResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> SyncAsync(
        [FromBody] SynchronizationRequestDto model, CancellationToken cancellationToken)
    {
        var request = WebApiMapper.Map(model);
        var response = await _service.SynchronizeAsync(request, cancellationToken);
        var dto = WebApiMapper.Map(response);

        return Ok(dto);
    }
}
