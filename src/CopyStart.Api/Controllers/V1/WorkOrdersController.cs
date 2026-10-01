using CopyStart.Application.WorkOrders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace CopyStart.Api.Controllers.V1;

[ApiController]
[Route("api/v1/work-orders")]
[Authorize]
public class WorkOrdersController : ControllerBase
{
    private readonly IMessageBus _bus;

    public WorkOrdersController(IMessageBus bus)
    {
        _bus = bus;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<WorkOrderDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(CancellationToken ct)
    {
        var result = await _bus.InvokeAsync<IReadOnlyList<WorkOrderDto>>(new ListWorkOrdersQuery(), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(WorkOrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _bus.InvokeAsync<WorkOrderDto?>(new GetWorkOrderByIdQuery(id), ct);
        if (result == null)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Work Order Not Found",
                Detail = $"Work order '{id}' was not found."
            });
        }
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(WorkOrderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateWorkOrderCommand command, CancellationToken ct)
    {
        var result = await _bus.InvokeAsync<WorkOrderDto>(command, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPost("{id:guid}/start")]
    [ProducesResponseType(typeof(WorkOrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Start(Guid id, [FromBody] ActorPayload? payload, CancellationToken ct)
    {
        var command = new StartWorkOrderCommand(id, payload?.Actor ?? "staff");
        var result = await _bus.InvokeAsync<WorkOrderDto>(command, ct);
        return Ok(result);
    }

    [HttpPost("{id:guid}/complete")]
    [ProducesResponseType(typeof(WorkOrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Complete(Guid id, [FromBody] CompleteOrderPayload payload, CancellationToken ct)
    {
        var command = new CompleteWorkOrderCommand(id, payload.ResolutionNotes, payload.Actor ?? "staff");
        var result = await _bus.InvokeAsync<WorkOrderDto>(command, ct);
        return Ok(result);
    }

    [HttpPost("{id:guid}/reset")]
    [ProducesResponseType(typeof(WorkOrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Reset(Guid id, [FromBody] ActorPayload? payload, CancellationToken ct)
    {
        var command = new ResetWorkOrderCommand(id, payload?.Actor ?? "staff");
        var result = await _bus.InvokeAsync<WorkOrderDto>(command, ct);
        return Ok(result);
    }
}

public record CompleteOrderPayload(string ResolutionNotes, string? Actor = null);
