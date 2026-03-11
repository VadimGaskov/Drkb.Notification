using Drkb.Notification.Application.UseCase.Command.CreateMessage;
using Drkb.Notification.Contract;
using MediatR;
using MessageBroker.Abstractions.Interfaces.Consumer;

namespace Drkb.Notification.Infrastructure.Services.MessageBroker;

public class MessageEventHandler: IEventHandler<MessageEvent>
{
    private readonly IMediator _mediator;

    public MessageEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task HandleAsync(MessageEvent @event, CancellationToken cancellationToken = new CancellationToken())
    {
        var message = new CreateMessageEvent()
        {
            EventId = @event.EventId,
            CreatedAt = @event.OccuredAt,
            PayloadJson = @event.PayloadJson,
            TypeNotification = @event.TypeNotification,
            UserIds = @event.UserIds,
        };
        await _mediator.Publish(message, cancellationToken);
    }
}