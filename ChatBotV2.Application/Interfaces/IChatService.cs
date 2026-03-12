using ChatBotV2.Domain.Entities;

namespace ChatBotV2.Application.Interfaces;

/// <summary>
/// Infrastructure service for OpenAI interaction.
/// Responsible only for technical communication with the external API.
/// </summary>
public interface IChatService
{
    Task<string> GetChatResponseAsync(ChatRequest request, ChatbotSettings settings, IEnumerable<HistoryMessage> history);
}