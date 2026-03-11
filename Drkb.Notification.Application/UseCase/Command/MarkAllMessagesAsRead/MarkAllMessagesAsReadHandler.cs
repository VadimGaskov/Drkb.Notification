using Drkb.Notification.Application.Interfaces.DataProvider;
using Drkb.ResultObjects;
using MediatR;

namespace Drkb.Notification.Application.UseCase.Command.MarkAllMessagesAsRead;

public class MarkAllMessagesAsReadHandler: IRequestHandler<MarkAllMessagesAsReadCommand, Result>
{
    private readonly IMarkAllMessagesAsReadDataProvider _markAllMessagesAsReadDataProvider;

    public MarkAllMessagesAsReadHandler(IMarkAllMessagesAsReadDataProvider markAllMessagesAsReadDataProvider, IUnitOfWork unitOfWork)
    {
        _markAllMessagesAsReadDataProvider = markAllMessagesAsReadDataProvider;
    }

    public async Task<Result> Handle(MarkAllMessagesAsReadCommand request, CancellationToken cancellationToken)
    {
        await _markAllMessagesAsReadDataProvider.ExecuteAsync(cancellationToken);
        return Result.Success();
    }
}