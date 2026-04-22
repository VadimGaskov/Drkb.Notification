using Drkb.Notification.Application.Interfaces.Authorization;
using Drkb.Notification.Application.UseCase.Command.MarkMessageByIdAsRead;
using Microsoft.EntityFrameworkCore;

namespace Drkb.Notification.Infrastructure.Data.Adapters;

public class EfMarkMessageByIdAsReadAdapter: IMarkMessageByIdAsReadPort
{
    private readonly ICurrentUserService _currentUserService;
    private readonly NotificationDbContext _context;

    public EfMarkMessageByIdAsReadAdapter(ICurrentUserService currentUserService, NotificationDbContext context)
    {
        _currentUserService = currentUserService;
        _context = context;
    }

    public async Task ExecuteAsync(MarkMessageByIdAsReadCommand command, CancellationToken cancellationToken)
    {
        await _context.Messages
            .Where(x => x.UserId == _currentUserService.UserId && x.Id == command.MessageId)
            .ExecuteUpdateAsync(
                x => x.SetProperty(m => m.IsRead, true),
                cancellationToken);
    }
}