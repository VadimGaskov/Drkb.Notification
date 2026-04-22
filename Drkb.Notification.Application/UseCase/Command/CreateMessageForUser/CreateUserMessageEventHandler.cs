using System.Text.Json;
using Drkb.Notification.Application.DTOs;
using Drkb.Notification.Application.Interfaces;
using Drkb.Notification.Application.Interfaces.DataProvider;
using Drkb.Notification.Application.UseCase.Command.CreateMessageForUser;
using Drkb.Notification.Domain.Entity;
using MediatR;

namespace Drkb.Notification.Application.UseCase.Command.CreateMessage;

public class CreateUserMessageEventHandler: INotificationHandler<CreateUserMessageEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICreateUserMessagePort _createUserMessagePort;
    private readonly INotificationDispatcher _notificationDispatcher;

    public CreateUserMessageEventHandler(IUnitOfWork unitOfWork, ICreateUserMessagePort createUserMessagePort, INotificationDispatcher notificationDispatcher)
    {
        _unitOfWork = unitOfWork;
        _createUserMessagePort = createUserMessagePort;
        _notificationDispatcher = notificationDispatcher;
    }

    public async Task Handle(CreateUserMessageEvent notification, CancellationToken cancellationToken)
    {
        var message = new Message
        {
            CreatedAt = DateTime.UtcNow,
            IsRead = false,
            PayloadJson = JsonSerializer.Serialize(notification.PayloadJson),
            Type = notification.TypeNotification
        };

        await _createUserMessagePort.AddAsync(message, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        var messageToAll = new MessageDto(message.PayloadJson, message.CreatedAt);

        await _notificationDispatcher.SendToUser(notification.UserId, message.Type, messageToAll, cancellationToken);
        
    }
}