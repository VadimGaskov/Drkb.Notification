using Drkb.ResultObjects;
using MediatR;

namespace Drkb.Notification.Application.UseCase.Command.MarkMessageByIdAsRead;

public record MarkMessageByIdAsReadCommand(Guid MessageId): IRequest<Result>;