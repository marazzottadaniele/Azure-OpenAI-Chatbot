using System.ComponentModel.DataAnnotations;

namespace ChatBotV2.Domain.Entities;

public class ChatbotSettings
{
    [Required]
    [StringLength(50, ErrorMessage = "Voice tone cannot exceed 50 characters")]
    public string VoiceTone { get; set; } = "kind and helpful";

    [StringLength(500, ErrorMessage = "Context cannot exceed 500 characters")]
    public string Context { get; set; } = "";

    [Required]
    [Range(1, 1000, ErrorMessage = "Maximum number of words must be between 1 and 1000")]
    public int MaxWords { get; set; } = 200;

    [Required]
    [StringLength(2000, ErrorMessage = "System message cannot exceed 2000 characters")]
    public string SystemMessage { get; set; } = "You are a helpful virtual assistant. Speak in Italian and maintain a professional tone. Be concise and helpful.";

    public string RequestSystemMessage
    {
        get => $"Your Tone of voice must be: {VoiceTone}. {SystemMessage}. This is the context within you should behave {Context}";
    }
}