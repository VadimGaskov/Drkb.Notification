using Drkb.Notification.Application.UseCase.Command.CreateBroadcastMessage;
using Drkb.Notification.Domain.Entity;

namespace Drkb.Notification.Infrastructure.Data.Adapters;

public class EfCreateBroadcastMessageAdapter: ICreateBroadcastMessagePort
{
    private readonly NotificationDbContext _context;

    public EfCreateBroadcastMessageAdapter(NotificationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(List<Message> entity, CancellationToken cancellationToken = default)
    {
        await _context.Messages.AddRangeAsync(entity, cancellationToken);
    }
}