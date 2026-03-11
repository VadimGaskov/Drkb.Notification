using Drkb.ResultObjects;
using MediatR;

namespace Drkb.Notification.Application.UseCase.Query.GetUnreadMessages;

public record GetUnreadMessagesQuery : IRequest<Result<List<GetUnreadMessagesDto>>>;
