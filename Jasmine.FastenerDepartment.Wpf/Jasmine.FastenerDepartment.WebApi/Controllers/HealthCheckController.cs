using Microsoft.AspNetCore.Mvc;

namespace Jasmine.FastenerDepartment.WebApi.Controllers;

/// <summary>
/// Health check controller.
/// </summary>
[ApiController]
[Route("health-check")]
public class HealthCheckController : ControllerBase
{
    /// <summary>
    /// Returns a success status code if the server status is OK.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult HealthCheck()
    {
        return Ok();
    }
}
