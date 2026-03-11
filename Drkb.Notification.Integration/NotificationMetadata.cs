namespace Drkb.Notification.Integration;

public class NotificationMetadata
{
    private const string ExchangeName = "notification-events";
    
    public static class Created
    {
        public const string EventName = "notification.created";
        public const string RoutingKey = "notification.created";
        public const string Exchange = ExchangeName;
    }
    
    public const string AllRoutingKeys = "notification.*";
}