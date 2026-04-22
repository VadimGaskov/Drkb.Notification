using Drkb.Notification.Application.UseCase.Command.CreateMessageForUser;
using Drkb.Notification.Domain.Entity;

namespace Drkb.Notification.Infrastructure.Data.Adapters;

public class EfCreateUserMessageAdapter: ICreateUserMessagePort
{
    private readonly NotificationDbContext _context;

    public EfCreateUserMessageAdapter(NotificationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Message entity, CancellationToken cancellationToken = default)
    {
        await _context.Messages.AddAsync(entity, cancellationToken);
    }
}