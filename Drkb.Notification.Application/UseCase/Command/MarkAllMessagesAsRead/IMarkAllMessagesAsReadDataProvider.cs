using Drkb.Notification.Application.Interfaces.DataProvider;

namespace Drkb.Notification.Application.UseCase.Command.MarkAllMessagesAsRead;

public interface IMarkAllMessagesAsReadDataProvider: IDataProviderMarker
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}