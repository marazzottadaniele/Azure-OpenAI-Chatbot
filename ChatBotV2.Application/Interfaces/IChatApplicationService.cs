using ChatBotV2.Application.Services;
using ChatBotV2.Domain.Entities;

namespace ChatBotV2.Application.Interfaces;

/// <summary>
/// Application service for chat management.
/// Contains business logic and orchestration.
/// </summary>
public interface IChatApplicationService
{
    Task<ChatResponseDto> HandleChatRequestAsync(ChatRequest request, IEnumerable<HistoryMessage> history);
}