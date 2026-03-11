using System.Text.Json.Serialization;

namespace Drkb.Notification.Application.DTOs;

public record BaseDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }
}
