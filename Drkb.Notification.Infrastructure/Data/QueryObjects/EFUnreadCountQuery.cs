using Drkb.Notification.Application.Interfaces.Authorization;
using Drkb.Notification.Application.UseCase.Query.GetUnreadCount;
using Microsoft.EntityFrameworkCore;

namespace Drkb.Notification.Infrastructure.Data.QueryObjects;

public class EFUnreadCountQuery: IUnreadCountQuery
{
    private readonly NotificationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public EFUnreadCountQuery(NotificationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<GetUnreadCountDto> ExecuteAsync(GetUnreadCountQuery query, CancellationToken cancellationToken = default)
    {
        var count = await _context.Messages
            .Where(x=>x.UserId == _currentUserService.UserId && !x.IsRead)
            .CountAsync(cancellationToken);
        
        return new GetUnreadCountDto(){Count = count};
    }
}