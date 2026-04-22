using MediatR;

namespace Drkb.Notification.Application.UseCase.Command.CreateMessageForUser;

public record CreateUserMessageEvent: INotification
{
    public Guid UserId { get; set; } = new();
    public object PayloadJson { get; set; } = null!;
    public string TypeNotification { get; set; } = null!;
}