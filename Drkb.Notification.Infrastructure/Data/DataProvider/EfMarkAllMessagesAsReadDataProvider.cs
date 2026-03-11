using Drkb.Notification.Application.Interfaces.Authorization;
using Drkb.Notification.Application.Interfaces.DataProvider;
using Drkb.Notification.Application.UseCase.Command.MarkAllMessagesAsRead;
using Microsoft.EntityFrameworkCore;

namespace Drkb.Notification.Infrastructure.Data.DataProvider;

public class EfMarkAllMessagesAsReadDataProvider: IMarkAllMessagesAsReadDataProvider
{
    private readonly NotificationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public EfMarkAllMessagesAsReadDataProvider(NotificationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await _context.Messages
            .Where(x => x.UserId == _currentUserService.UserId && !x.IsRead)
            .ExecuteUpdateAsync(
                x => x.SetProperty(m => m.IsRead, true), cancellationToken);
    }
}