using Azure.AI.OpenAI;
using ChatBotV2.Application.Interfaces;
using ChatBotV2.Domain.Entities;
using ChatBotV2.Domain.Enums;

namespace ChatBotV2.Infrastructure.Services;

public class ChatService : IChatService
{
    private readonly OpenAIClient _client;
    private readonly string _deployment;

    public ChatService(OpenAIClient client, string deployment)
    {
        _client = client;
        _deployment = deployment;
    }

    public async Task<string> GetChatResponseAsync(ChatRequest request, ChatbotSettings settings, IEnumerable<HistoryMessage> history)
    {
        if (request.Message.Split(' ').Length > settings.MaxWords)
        {
            throw new ArgumentException($"Message exceeds {settings.MaxWords} words.");
        }
        
        // System message
        var messages = new List<ChatRequestMessage>
        {
            new ChatRequestSystemMessage(settings.RequestSystemMessage)
        };

        // History messages
        foreach (var historyMessage in history)
        {
            if (historyMessage.Role == HistoryRole.Assistant)
            {
                messages.Add(new ChatRequestAssistantMessage(historyMessage.Message));
            }
            else
            {
                messages.Add(new ChatRequestUserMessage(historyMessage.Message));
            }
        }

        // Current user message
        messages.Add(new ChatRequestUserMessage(request.Message));

        var completionOptions = new ChatCompletionsOptions(_deployment, messages)
        {
            MaxTokens = 150,
            Temperature = 0.7f,
        };

        var response = await _client.GetChatCompletionsAsync(completionOptions);
        return response.Value.Choices[0].Message.Content;
    }
}