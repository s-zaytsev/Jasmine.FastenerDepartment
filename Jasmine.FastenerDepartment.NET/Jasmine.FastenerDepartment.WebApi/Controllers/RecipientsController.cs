using Jasmine.FastenerDepartment.Domain.Recipients.Services;
using Jasmine.FastenerDepartment.WebApi.Dtos.Recipients;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.FastenerDepartment.WebApi.Controllers;

/// <summary>
/// Recipients controller.
/// </summary>
[ApiController]
[Route("recipients")]
public class RecipientsController : ControllerBase
{
    private readonly IRecipientsService _recipientsService;
    private readonly WebApiMapper _mapper;

    /// <summary>
    /// Creates controller.
    /// </summary>
    /// <param name="recipientsService">Recipients service.</param>
    /// <param name="mapper">Mapper.</param>
    public RecipientsController(
        IRecipientsService recipientsService,
        WebApiMapper mapper)
    {
        _recipientsService = recipientsService;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns a collection of recipients.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of recipients.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RecipientDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRecipientsAsync(CancellationToken cancellationToken)
    {
        var recipients = await _recipientsService.GetAllAsync(cancellationToken);
        var dtos = recipients.Select(_mapper.Map);

        return Ok(dtos);
    }

    /// <summary>
    /// Creates a recipients.
    /// </summary>
    /// <param name="dto">Change recipient model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Recipient identifier.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateRecipientAsync(
        [FromBody] ChangeRecipientDto dto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(dto);
        var recipient = await _recipientsService.CreateAsync(model, cancellationToken);

        return Ok(recipient.Id);
    }

    /// <summary>
    /// Updates a recipient.
    /// </summary>
    /// <param name="id">Recipient identifier.</param>
    /// <param name="dto">Change recipient model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateRecipientAsync(
        [FromRoute] Guid id, [FromBody] ChangeRecipientDto dto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(dto);
        await _recipientsService.UpdateAsync(id, model, cancellationToken);

        return NoContent();
    }
}
