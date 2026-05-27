using Jasmine.FastenerDepartment.Domain.Settings.Services;
using Jasmine.FastenerDepartment.WebApi.Dtos.SettingsEntries;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.FastenerDepartment.WebApi.Controllers;

/// <summary>
/// Settings controller.
/// </summary>
[ApiController]
[Route("settings")]
public class SettingsController : ControllerBase
{
    private readonly ISettingsEntriesService _settingsService;
    private readonly WebApiMapper _mapper;

    /// <summary>
    /// Creates controller.
    /// </summary>
    /// <param name="settingsService">Settings service.</param>
    /// <param name="mapper">Mapper.</param>
    public SettingsController(
        ISettingsEntriesService settingsService,
        WebApiMapper mapper)
    {
        _settingsService = settingsService;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns company settings.
    /// </summary>
    [HttpGet("company")]
    [ProducesResponseType(typeof(CompanySettingsDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCompanySettingsAsync()
    {
        var settings = await _settingsService.GetCompanySettingsAsync();
        var dtos = _mapper.Map(settings);

        return Ok(dtos);
    }

    /// <summary>
    /// Changes company settings.
    /// </summary>
    /// <param name="dto">Change company settings model.</param>
    [HttpPut("company")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ChangeCompanySettingsAsync([FromBody] ChangeCompanySettingsDto dto)
    {
        var settings = _mapper.Map(dto);
        await _settingsService.ChangeCompanySettingsAsync(settings);

        return NoContent();
    }

    /// <summary>
    /// Returns email settings.
    /// </summary>
    [HttpGet("email")]
    [ProducesResponseType(typeof(EmailSettingsDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEmailSettingsAsync()
    {
        var settings = await _settingsService.GetEmailSettingsAsync();
        var dtos = _mapper.Map(settings);

        return Ok(dtos);
    }

    /// <summary>
    /// Changes email settings.
    /// </summary>
    /// <param name="dto">Change email settings model.</param>
    [HttpPut("email")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ChangeEmailSettingsAsync([FromBody] ChangeEmailSettingsDto dto)
    {
        var settings = _mapper.Map(dto);
        await _settingsService.ChangeEmailSettingsAsync(settings);

        return NoContent();
    }
}
