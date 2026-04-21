using MassTransit;

namespace Drkb.Notification.Contract;

[EntityName("task.created.v1")]
public record MessageEvent
{
    public List<Guid> UserIds { get; set; } = new();
    public object PayloadJson { get; set; } = null!;
    public string TypeNotification { get; set; } = null!;
}