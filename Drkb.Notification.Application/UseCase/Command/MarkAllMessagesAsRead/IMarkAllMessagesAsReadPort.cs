using Drkb.Notification.Application.Interfaces.DataProvider;

namespace Drkb.Notification.Application.UseCase.Command.MarkAllMessagesAsRead;

public interface IMarkAllMessagesAsReadPort: IPortMarker
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}