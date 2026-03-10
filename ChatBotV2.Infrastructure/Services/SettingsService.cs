using System.Text.Json;
using ChatBotV2.Application.Interfaces;
using ChatBotV2.Domain.Entities;

namespace ChatBotV2.Infrastructure.Services;

public class SettingsService : ISettingsService
{
    private readonly string _settingsFilePath;
    private ChatbotSettings _settings;

    public SettingsService()
    {
        _settingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "chatbot-settings.json");
        _settings = LoadSettings();
    }

    public ChatbotSettings GetSettings() => _settings;

    public void UpdateSettings(ChatbotSettings newSettings)
    {
        _settings = newSettings;
        SaveSettings();
    }

    private ChatbotSettings LoadSettings()
    {
        if (File.Exists(_settingsFilePath))
        {
            try
            {
                var json = File.ReadAllText(_settingsFilePath);
                return JsonSerializer.Deserialize<ChatbotSettings>(json) ?? new ChatbotSettings();
            }
            catch
            {
                // Se c'è un errore, usa valori di default
                return new ChatbotSettings();
            }
        }
        return new ChatbotSettings();
    }

    private void SaveSettings()
    {
        try
        {
            var json = JsonSerializer.Serialize(_settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_settingsFilePath, json);
        }
        catch (Exception ex)
        {
            // Log dell'errore se necessario
            Console.WriteLine($"Errore nel salvataggio delle impostazioni: {ex.Message}");
        }
    }
}