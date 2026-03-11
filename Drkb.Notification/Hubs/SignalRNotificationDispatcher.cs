using Drkb.Notification.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Drkb.Notification.Hubs;

public class SignalRNotificationDispatcher: INotificationDispatcher
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public SignalRNotificationDispatcher(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendToUser(Guid userId, string typeNotification, object notification, CancellationToken cancellationToken)
    {
        await _hubContext.Clients.User(userId.ToString())
            .SendAsync(typeNotification, notification, cancellationToken);
    }

    public async Task SendToUsers(IEnumerable<Guid> userIds, string typeNotification, object notification,
        CancellationToken cancellationToken)
    {
        await _hubContext.Clients.Users(userIds.Select(u => u.ToString()).ToList())
            .SendAsync(typeNotification, notification, cancellationToken);
    }

    public async Task SendToAll(string typeNotification, object notification, CancellationToken cancellationToken)
    {
        await _hubContext.Clients.All.SendAsync(typeNotification, notification, cancellationToken);
    }
}