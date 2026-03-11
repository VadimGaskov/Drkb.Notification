using Drkb.ResultObjects;
using MediatR;

namespace Drkb.Notification.Application.UseCase.Command.MarkAllMessagesAsRead;

public record MarkAllMessagesAsReadCommand(): IRequest<Result>;