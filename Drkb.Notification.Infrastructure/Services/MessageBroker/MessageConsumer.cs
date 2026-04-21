using Drkb.Notification.Application.UseCase.Command.CreateMessage;
using Drkb.Notification.Contract;
using MassTransit;
using MediatR;
using MessageBroker.Abstractions.Interfaces.Consumer;

namespace Drkb.Notification.Infrastructure.Services.MessageBroker;

public class MessageConsumer: IConsumer<MessageEvent>
{
    private readonly IMediator _mediator;

    public MessageConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<MessageEvent> context)
    {
        var @event = context.Message;
        
        var message = new CreateMessageEvent()
        {
            PayloadJson = @event.PayloadJson,
            TypeNotification = @event.TypeNotification,
            UserIds = @event.UserIds,
        };
        
        await _mediator.Publish(message);
    }
}