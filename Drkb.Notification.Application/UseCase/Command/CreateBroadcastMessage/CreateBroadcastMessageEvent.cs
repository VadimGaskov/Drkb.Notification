using MediatR;

namespace Drkb.Notification.Application.UseCase.Command.CreateMessage;

public record CreateBroadcastMessageEvent: INotification
{
    public List<Guid> UserIds { get; set; } = new();
    public object Payload { get; set; } = null!;
    public string TypeNotification { get; set; } = null!;
}