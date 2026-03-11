namespace Drkb.Notification.Application.UseCase.Command.CreateMessage;

public record MessageDto(string Type, object PayloadJson, DateTime CreatedAt);