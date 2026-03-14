using ChatBotV2.Application.DTOs.Responses;
using ChatBotV2.Application.Interfaces;
using ChatBotV2.Domain.Entities;

namespace ChatBotV2.Application.Services;

/// <summary>
/// Application service that handles business logic for chat.
/// Orchestrates multiple components and applies business rules.
/// </summary>
public class ChatApplicationService : IChatApplicationService
{
    private readonly IChatService _chatService;
    private readonly ISettingsService _settingsService;

    public ChatApplicationService(IChatService chatService, ISettingsService settingsService)
    {
        _chatService = chatService;
        _settingsService = settingsService;
    }

    /// <summary>
    /// Handles a chat request by applying business rules.
    /// </summary>
    public async Task<ChatResponseDto> HandleChatRequestAsync(ChatRequest request, IEnumerable<HistoryMessage> history)
    {
        var settings = _settingsService.GetSettings();

        // Business rule: Validate message length
        if (request.Message.Split(' ').Length > settings.MaxWords)
        {
            throw new ArgumentException($"Message exceeds the limit of {settings.MaxWords} words.");
        }

        // Business rule: Check if message contains forbidden words
        if (ContainsForbiddenWords(request.Message))
        {
            throw new ArgumentException("Message contains prohibited content.");
        }

        // Business rule: Rate limiting logic (simplified example)
        if (IsRateLimited())
        {
            throw new InvalidOperationException("Too many requests. Please try again later.");
        }

        // Call the infrastructure service to get the response
        var response = await _chatService.GetChatResponseAsync(request, settings, history);

        return new ChatResponseDto
        {
            Content = response,
            Timestamp = DateTime.UtcNow,
            WordCount = response.Split(' ').Length
        };
    }

    private bool ContainsForbiddenWords(string message)
    {
        // Simplified implementation - would use a forbidden words list in reality
        var forbiddenWords = new[] { "fingi", "pretend", "ignora", "ignore", "prompt" };
        return forbiddenWords.Any(word => message.Contains(word, StringComparison.OrdinalIgnoreCase));
    }

    private bool IsRateLimited()
    {
        // Simplified implementation - would check user counter in reality
        return false; // Always false for now
    }


}

