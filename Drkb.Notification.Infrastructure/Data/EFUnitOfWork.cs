using Drkb.Notification.Application.Interfaces.DataProvider;

namespace Drkb.Notification.Infrastructure.Data;

public class EFUnitOfWork: IUnitOfWork
{
    private readonly NotificationDbContext _context;

    public EFUnitOfWork(NotificationDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}