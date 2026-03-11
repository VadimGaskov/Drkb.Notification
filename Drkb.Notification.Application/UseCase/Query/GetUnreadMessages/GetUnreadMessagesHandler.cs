using Drkb.ResultObjects;
using MediatR;

namespace Drkb.Notification.Application.UseCase.Query.GetUnreadMessages;

public class GetUnreadMessagesHandler: IRequestHandler<GetUnreadMessagesQuery, Result<List<GetUnreadMessagesDto>>>
{
    private readonly IUnreadMessagesQuery _unreadMessagesQuery;

    public GetUnreadMessagesHandler(IUnreadMessagesQuery unreadMessagesQuery)
    {
        _unreadMessagesQuery = unreadMessagesQuery;
    }

    public async Task<Result<List<GetUnreadMessagesDto>>> Handle(GetUnreadMessagesQuery request, CancellationToken cancellationToken)
    {
        var unreadMessages = await _unreadMessagesQuery.ExecuteAsync(request, cancellationToken);
        return Result<List<GetUnreadMessagesDto>>.Success(unreadMessages);
    }
}