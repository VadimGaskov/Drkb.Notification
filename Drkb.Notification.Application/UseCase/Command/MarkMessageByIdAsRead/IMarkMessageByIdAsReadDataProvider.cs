using Drkb.Notification.Application.Interfaces.DataProvider;

namespace Drkb.Notification.Application.UseCase.Command.MarkMessageByIdAsRead;

public interface IMarkMessageByIdAsReadDataProvider: IDataProviderMarker
{
    Task ExecuteAsync(MarkMessageByIdAsReadCommand command, CancellationToken cancellationToken);
}