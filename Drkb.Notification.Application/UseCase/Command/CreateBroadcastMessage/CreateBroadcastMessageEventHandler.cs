using System.Text.Json;
using Drkb.Notification.Application.DTOs;
using Drkb.Notification.Application.Interfaces;
using Drkb.Notification.Application.Interfaces.DataProvider;
using Drkb.Notification.Application.UseCase.Command.CreateMessage;
using Drkb.Notification.Domain.Entity;
using MediatR;

namespace Drkb.Notification.Application.UseCase.Command.CreateBroadcastMessage;

public class CreateBroadcastMessageEventHandler: INotificationHandler<CreateBroadcastMessageEvent>
{
    private readonly ICreateBroadcastMessagePort _createBroadcastMessagePort;
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationDispatcher _notificationDispatcher;

    public CreateBroadcastMessageEventHandler(ICreateBroadcastMessagePort createBroadcastMessagePort, IUnitOfWork unitOfWork, INotificationDispatcher notificationDispatcher)
    {
        _createBroadcastMessagePort = createBroadcastMessagePort;
        _unitOfWork = unitOfWork;
        _notificationDispatcher = notificationDispatcher;
    }

    public async Task Handle(CreateBroadcastMessageEvent notification, CancellationToken cancellationToken)
    {
        var userIds = notification.UserIds.Distinct().ToList();

        var messages = userIds
            .Select(userId => new Message
            {
                CreatedAt = DateTime.UtcNow,
                IsRead = false,
                PayloadJson = JsonSerializer.Serialize(notification.Payload),
                Type = notification.TypeNotification,
                UserId = userId
            })
            .ToList();

        await _createBroadcastMessagePort.AddAsync(messages, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var messageToUsers = new MessageDto(notification.Payload, DateTime.UtcNow);

        await _notificationDispatcher.SendToUsers(userIds, notification.TypeNotification, messageToUsers, cancellationToken);
    }
}