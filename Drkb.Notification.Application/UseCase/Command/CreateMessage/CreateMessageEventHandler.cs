using System.Text.Json;
using Drkb.Notification.Application.Interfaces;
using Drkb.Notification.Application.Interfaces.DataProvider;
using Drkb.Notification.Domain.Entity;
using MediatR;

namespace Drkb.Notification.Application.UseCase.Command.CreateMessage;

public class CreateMessageEventHandler: INotificationHandler<CreateMessageEvent>
{
    private readonly ICreateMessageDataProvider _createMessageDataProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationDispatcher _notificationDispatcher;

    public CreateMessageEventHandler(ICreateMessageDataProvider createMessageDataProvider, IUnitOfWork unitOfWork, INotificationDispatcher notificationDispatcher)
    {
        _createMessageDataProvider = createMessageDataProvider;
        _unitOfWork = unitOfWork;
        _notificationDispatcher = notificationDispatcher;
    }

    public async Task Handle(CreateMessageEvent notification, CancellationToken cancellationToken)
    {
        if (notification.UserIds.Count == 0)
        {
            var message = new Message
            {
                CreatedAt = DateTime.UtcNow,
                IsRead = false,
                PayloadJson = JsonSerializer.Serialize(notification.PayloadJson),
                Type = notification.TypeNotification
            };

            await _createMessageDataProvider.AddAsync(message, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var messageToAll = new MessageDto(message.PayloadJson, message.CreatedAt);

            await _notificationDispatcher.SendToAll(message.Type, messageToAll, cancellationToken);
            return;
        }

        var userIds = notification.UserIds.Distinct().ToList();

        var messages = userIds
            .Select(userId => new Message
            {
                CreatedAt = DateTime.UtcNow,
                IsRead = false,
                PayloadJson = JsonSerializer.Serialize(notification.PayloadJson),
                Type = notification.TypeNotification,
                UserId = userId
            })
            .ToList();

        await _createMessageDataProvider.AddAsync(messages, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var messageToUsers = new MessageDto(notification.PayloadJson, DateTime.UtcNow);

        await _notificationDispatcher.SendToUsers(userIds, notification.TypeNotification, messageToUsers, cancellationToken);
    }
}