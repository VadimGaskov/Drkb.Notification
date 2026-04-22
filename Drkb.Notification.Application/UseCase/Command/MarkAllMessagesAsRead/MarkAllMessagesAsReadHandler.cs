using Drkb.Notification.Application.Interfaces.DataProvider;
using Drkb.ResultObjects;
using MediatR;

namespace Drkb.Notification.Application.UseCase.Command.MarkAllMessagesAsRead;

public class MarkAllMessagesAsReadHandler: IRequestHandler<MarkAllMessagesAsReadCommand, Result>
{
    private readonly IMarkAllMessagesAsReadPort _markAllMessagesAsReadPort;

    public MarkAllMessagesAsReadHandler(IMarkAllMessagesAsReadPort markAllMessagesAsReadPort, IUnitOfWork unitOfWork)
    {
        _markAllMessagesAsReadPort = markAllMessagesAsReadPort;
    }

    public async Task<Result> Handle(MarkAllMessagesAsReadCommand request, CancellationToken cancellationToken)
    {
        await _markAllMessagesAsReadPort.ExecuteAsync(cancellationToken);
        return Result.Success();
    }
}