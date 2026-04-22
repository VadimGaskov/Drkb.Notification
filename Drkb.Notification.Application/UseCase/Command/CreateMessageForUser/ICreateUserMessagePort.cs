using Drkb.Notification.Application.Interfaces.DataProvider;
using Drkb.Notification.Domain.Entity;

namespace Drkb.Notification.Application.UseCase.Command.CreateMessageForUser;

public interface ICreateUserMessagePort: IPortMarker, 
    IAddPort<Message>
{
    
}