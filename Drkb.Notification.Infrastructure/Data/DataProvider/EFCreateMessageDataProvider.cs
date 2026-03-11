using Drkb.Notification.Application.Interfaces.DataProvider;
using Drkb.Notification.Application.UseCase.Command.CreateMessage;
using Drkb.Notification.Domain.Entity;

namespace Drkb.Notification.Infrastructure.Data.DataProvider;

public class EFCreateMessageDataProvider: ICreateMessageDataProvider
{
    private readonly NotificationDbContext _context;

    public EFCreateMessageDataProvider(NotificationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(List<Message> entity, CancellationToken cancellationToken = default)
    {
        await _context.Messages.AddRangeAsync(entity, cancellationToken);
    }

    public async Task AddAsync(Message entity, CancellationToken cancellationToken = default)
    {
        await _context.Messages.AddAsync(entity, cancellationToken);
    }
}