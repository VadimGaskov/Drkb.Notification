using System.Text.Json;
using Drkb.Employment.Contract;
using Drkb.Notification.Application.Constants;
using Drkb.Notification.Application.DTOs.Employment;
using Drkb.Notification.Application.UseCase.Command.CreateMessage;
using MassTransit;
using MediatR;

namespace Drkb.Notification.Infrastructure.Services.MessageBroker.Employment;

public class ElementCompletedConsumer: IConsumer<ElementCompletedEvent>
{
    private readonly IMediator _mediator;

    public ElementCompletedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ElementCompletedEvent> context)
    {
        var @event = context.Message;

        var payload = new ElementCompletedPayload();
        
        var message = new CreateBroadcastMessageEvent()
        {
            Payload = payload,
            TypeNotification = TypeNotification.CompletedElementEmployment,
            UserIds = @event.UserIds,
        };
        
        await _mediator.Publish(message);
    }
}