using Drkb.ResultObjects;
using MediatR;

namespace Drkb.Notification.Application.UseCase.Query.GetUnreadCount;

public class GetUnreadCountHandler: IRequestHandler<GetUnreadCountQuery, Result<GetUnreadCountDto>>
{
    private readonly IUnreadCountQuery _query;

    public GetUnreadCountHandler(IUnreadCountQuery query)
    {
        _query = query;
    }

    public async Task<Result<GetUnreadCountDto>> Handle(GetUnreadCountQuery request, CancellationToken cancellationToken)
    {
        var count = await _query.ExecuteAsync(request, cancellationToken);
        return Result<GetUnreadCountDto>.Success(count);
    }
}