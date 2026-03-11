namespace Drkb.Notification.Application.Interfaces;

public interface INotificationDispatcher
{
    /// <summary>
    /// Отправка конкретному пользователю
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="typeNotification"></param>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendToUser(Guid userId, string typeNotification, object notification, CancellationToken cancellationToken);
    
    /// <summary>
    /// Отправка множеству пользователей
    /// </summary>
    /// <param name="userIds"></param>
    /// <param name="typeNotification"></param>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendToUsers(IEnumerable<Guid> userIds, string typeNotification, object notification, CancellationToken cancellationToken);
    
    /// <summary>
    /// Отправка всем подписавшимся соединениям
    /// </summary>
    /// <param name="typeNotification"></param>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendToAll(string typeNotification, object notification, CancellationToken cancellationToken);
}