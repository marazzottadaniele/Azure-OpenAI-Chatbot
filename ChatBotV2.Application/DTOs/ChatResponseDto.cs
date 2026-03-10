/// <summary>
/// DTO for chat response (Application Layer)
/// </summary>
public class ChatResponseDto
{
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public int WordCount { get; set; }
}