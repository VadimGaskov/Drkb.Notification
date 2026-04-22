using System.Text.Json;
using Drkb.Employment.Contract;
using Drkb.Notification.Application.Constants;
using Drkb.Notification.Application.DTOs.Employment;
using Drkb.Notification.Application.UseCase.Command.CreateMessage;
using MassTransit;
using MediatR;

namespace Drkb.Notification.Infrastructure.Services.MessageBroker.Employment;

public class CheckListCoursesAssignedConsumer: IConsumer<CheckListCoursesAssignedEvent>
{
    private readonly IMediator _mediator;

    public CheckListCoursesAssignedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CheckListCoursesAssignedEvent> context)
    {
        var @event = context.Message;

        var payload = new CheckListCoursesAssignedPayload
            { Title = "Новый сотрудник", Description = "Вам назначен новый сотрудник" };
        
        var message = new CreateBroadcastMessageEvent()
        {
            Payload = payload,
            TypeNotification = TypeNotification.AssignedNewUsers,
            UserIds = @event.ResponsibleUserIds.ToList(),
        };
        
        await _mediator.Publish(message);
    }
}