using Drkb.ResultObjects;
using MediatR;

namespace Drkb.Notification.Application.UseCase.Query.GetUnreadCount;

public record GetUnreadCountQuery() : IRequest<Result<GetUnreadCountDto>>;
