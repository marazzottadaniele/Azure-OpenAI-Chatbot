using ChatBotV2.Domain.Entities;

namespace ChatBotV2.Application.DTOs.Requests;

/// <summary>
/// DTO for chat request input (Application Layer)
/// </summary>
public class ChatRequestDto
{
    public required ChatRequest Request { get; set; }
    public required IEnumerable<HistoryMessage> History { get; set; }
}