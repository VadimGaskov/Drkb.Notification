using MessageBroker.Abstractions;

namespace Drkb.Notification.Contract;

public record MessageEvent: BaseIntegrationEvent
{
    public List<Guid> UserIds { get; set; } = new();
    public string PayloadJson { get; set; } = null!;
    public string TypeNotification { get; set; } = null!;
}