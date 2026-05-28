using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Orders.Services;
using Jasmine.FastenerDepartment.WebApi.Dtos.Orders;
using Jasmine.FastenerDepartment.WebApi.Dtos.Products;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.FastenerDepartment.WebApi.Controllers;

/// <summary>
/// Orders controller.
/// </summary>
[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _ordersService;
    private readonly WebApiMapper _mapper;

    /// <summary>
    /// Creates controller.
    /// </summary>
    /// <param name="ordersService">Orders service.</param>
    /// <param name="mapper">Mapper.</param>
    public OrdersController(
        IOrdersService ordersService,
        WebApiMapper mapper)
    {
        _ordersService = ordersService;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns the list of orders.
    /// </summary>
    /// <param name="queryDto">Orders query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of orders.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Page<OrderDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPageAsync(
        [FromQuery] OrdersQueryDto queryDto, CancellationToken cancellationToken)
    {
        var query = _mapper.Map(queryDto);
        var page = await _ordersService.GetPageAsync(query, cancellationToken);
        var dtos = _mapper.Map(page);

        return Ok(dtos);
    }

    /// <summary>
    /// Returns the order.
    /// </summary>
    /// <param name="id">Order identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Order.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrderAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var order = await _ordersService.GetAsync(id, cancellationToken);
        var dto = _mapper.Map(order);

        return Ok(dto);
    }

    /// <summary>
    /// Creates the order.
    /// </summary>
    /// <param name="dto">Create order model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Order identifier.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateOrderAsync(
        [FromBody] CreateOrderDto dto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(dto);

        var order = await _ordersService.CreateAsync(model, cancellationToken);
        return Ok(order.Id);
    }

    /// <summary>
    /// Changes the order.
    /// </summary>
    /// <param name="id">Order identifier.</param>
    /// <param name="dto">Change order model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ChangeOrderAsync(
        [FromRoute] Guid id, [FromBody] ChangeOrderDto dto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(dto);
        await _ordersService.UpdateAsync(id, model, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Completes the order.
    /// </summary>
    /// <param name="id">Order identifier.</param>
    /// <param name="dto">Send order model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPost("{id}/send")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> SendOrderAsync(
        [FromRoute] Guid id, [FromBody] SendOrderModelDto dto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(dto);
        await _ordersService.SendAsync(id, model, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Completes the order.
    /// </summary>
    /// <param name="id">Order identifier.</param>
    /// <param name="dto">Complete order model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPost("{id}/complete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CompleteOrderAsync(
        [FromRoute] Guid id, [FromBody] CompleteOrderDto dto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(dto);
        await _ordersService.CompleteAsync(id, model, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Cancels the order.
    /// </summary>
    /// <param name="id">Order identifier.</param>
    /// <param name="dto">Cancel order model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPost("{id}/cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CancelOrderAsync(
        [FromRoute] Guid id, [FromBody] CancelOrderDto dto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(dto);
        await _ordersService.CancelAsync(id, model, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Returns an order document.
    /// </summary>
    /// <param name="id">Order identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>File.</returns>
    [HttpGet("document/{id}")]
    [ProducesResponseType(typeof(File), StatusCodes.Status200OK)]
    public async Task<IActionResult> DownloadDocumentAsync(
        [FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var response = await _ordersService.GetOrderDocumentStreamAsync(id, cancellationToken);

        return File(response.Stream, "application/octet-stream", $"{response.Name}.docx");
    }
}
