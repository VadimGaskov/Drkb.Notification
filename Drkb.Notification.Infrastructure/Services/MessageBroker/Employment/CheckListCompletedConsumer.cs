using System.Text.Json;
using Drkb.Employment.Contract;
using Drkb.Notification.Application.Constants;
using Drkb.Notification.Application.DTOs;
using Drkb.Notification.Application.UseCase.Command.CreateMessageForUser;
using MassTransit;
using MediatR;

namespace Drkb.Notification.Infrastructure.Services.MessageBroker.Employment;

public class CheckListCompletedConsumer: IConsumer<CheckListCompletedEvent>
{
    private readonly IMediator _mediator;

    public CheckListCompletedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CheckListCompletedEvent> context)
    {
        var @event = context.Message;

        var payload = new CompleteUserCheckListPayload();
        
        var message = new CreateUserMessageEvent()
        {
            PayloadJson = payload,
            TypeNotification = TypeNotification.CheckListCompleted,
            UserId = @event.UserId,
        };
        
        await _mediator.Publish(message);
    }
}