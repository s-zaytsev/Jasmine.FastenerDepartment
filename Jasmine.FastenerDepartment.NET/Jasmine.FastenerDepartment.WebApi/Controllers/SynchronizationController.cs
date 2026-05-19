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
    private readonly WebApiMapper _mapper;

    /// <summary>
    /// Creates controller.
    /// </summary>
    /// <param name="service">Synchronization service.</param>
    /// <param name="mapper">Mapper.</param>
    public SynchronizationController(
        ISynchronizationService service,
        WebApiMapper mapper)
    {
        _service = service;
        _mapper = mapper;
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
        var request = _mapper.Map(model);
        var response = await _service.SynchronizeAsync(request, cancellationToken);
        var dto = _mapper.Map(response);

        return Ok(dto);
    }
}
