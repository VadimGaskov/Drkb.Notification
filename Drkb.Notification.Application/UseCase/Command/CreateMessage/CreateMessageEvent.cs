using MediatR;

namespace Drkb.Notification.Application.UseCase.Command.CreateMessage;

public record CreateMessageEvent: INotification
{
    public List<Guid> UserIds { get; set; } = new();
    public string PayloadJson { get; set; } = null!;
    public string TypeNotification { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public Guid EventId { get; set; }
}