using Drkb.Notification.Application.Interfaces.Authorization;
using Drkb.Notification.Application.UseCase.Query.GetUnreadMessages;
using Microsoft.EntityFrameworkCore;

namespace Drkb.Notification.Infrastructure.Data.QueryObjects;

public class EFUnreadMessagesQuery: IUnreadMessagesQuery
{
    private readonly NotificationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public EFUnreadMessagesQuery(ICurrentUserService currentUserService, NotificationDbContext context)
    {
        _currentUserService = currentUserService;
        _context = context;
    }

    public async Task<List<GetUnreadMessagesDto>> ExecuteAsync(GetUnreadMessagesQuery query, CancellationToken cancellationToken = default)
    {
        return await _context.Messages
            .AsNoTracking()
            .Where(x => x.UserId == _currentUserService.UserId && !x.IsRead)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new GetUnreadMessagesDto
            {
                Id = x.Id,
                Type = x.Type,
                CreatedAt = x.CreatedAt,
                PayloadJson = x.PayloadJson,
            })
            .ToListAsync(cancellationToken);
    }
}