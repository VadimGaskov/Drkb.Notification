using MediatR;

namespace Drkb.Notification.Application.UseCase.Command.CreateMessage;

public record CreateMessageEvent: INotification
{
    public List<Guid> UserIds { get; set; } = new();
    public object PayloadJson { get; set; } = null!;
    public string TypeNotification { get; set; } = null!;
}