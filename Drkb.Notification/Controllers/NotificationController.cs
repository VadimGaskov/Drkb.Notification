using Drkb.Notification.Application.UseCase.Command.MarkAllMessagesAsRead;
using Drkb.Notification.Application.UseCase.Command.MarkMessageByIdAsRead;
using Drkb.Notification.Application.UseCase.Query.GetUnreadCount;
using Drkb.Notification.Application.UseCase.Query.GetUnreadMessages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Drkb.Notification.Controllers;

[Authorize]
[ApiController]
[Route("api/notifications")]
public class NotificationController: ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<GetUnreadMessagesDto>>> Get(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetUnreadMessagesQuery(), ct);
        if (!result.IsSuccess)
        {
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }

        return result.Data;
    }

    [HttpGet("unread-count")]
    public async Task<ActionResult<GetUnreadCountDto>> GetUnreadCount(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetUnreadCountQuery(), ct);
        if (!result.IsSuccess)
        {
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }

        return result.Data;
    }

    [HttpPost("{id:guid}/read")]
    public async Task<IActionResult> MarkAsRead(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new MarkMessageByIdAsReadCommand(id), ct);
        if (!result.IsSuccess)
        {
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }

        return Ok();
    }

    [HttpPost("read-all")]
    public async Task<ActionResult> MarkAllAsRead(CancellationToken ct)
    {
        var result = await _mediator.Send(new MarkAllMessagesAsReadCommand(), ct);
        if (!result.IsSuccess)
        {
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }

        return Ok();
    }
}