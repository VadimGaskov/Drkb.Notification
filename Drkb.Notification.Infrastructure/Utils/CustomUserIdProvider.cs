using Microsoft.AspNetCore.SignalR;

namespace Drkb.Notification.Infrastructure.Utils;

public class CustomUserIdProvider: IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.User?.FindFirst("id")?.Value;
    }
}