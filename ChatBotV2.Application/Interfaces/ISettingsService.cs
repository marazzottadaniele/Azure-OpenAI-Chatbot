using ChatBotV2.Domain.Entities;

namespace ChatBotV2.Application.Interfaces;

public interface ISettingsService
{
    ChatbotSettings GetSettings();
    void UpdateSettings(ChatbotSettings newSettings);
}