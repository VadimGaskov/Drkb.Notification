using System.Text.Json.Serialization;

namespace Drkb.Notification.Application.DTOs.Employment;

public class CheckListCoursesAssignedPayload
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
}