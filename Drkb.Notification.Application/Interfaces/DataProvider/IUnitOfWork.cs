namespace Drkb.Notification.Application.Interfaces.DataProvider;

public interface IUnitOfWork: IPortMarker
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
}