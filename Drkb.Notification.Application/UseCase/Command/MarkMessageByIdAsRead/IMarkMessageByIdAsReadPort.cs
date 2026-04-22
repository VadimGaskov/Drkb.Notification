using Drkb.Notification.Application.Interfaces.DataProvider;

namespace Drkb.Notification.Application.UseCase.Command.MarkMessageByIdAsRead;

public interface IMarkMessageByIdAsReadPort: IPortMarker
{
    Task ExecuteAsync(MarkMessageByIdAsReadCommand command, CancellationToken cancellationToken);
}