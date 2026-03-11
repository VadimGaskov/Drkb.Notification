namespace Drkb.Notification.Application.Interfaces.DataProvider;

public interface IUnitOfWork: IDataProviderMarker
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
}