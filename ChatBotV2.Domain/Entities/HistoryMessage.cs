using ChatBotV2.Domain.Enums;

namespace ChatBotV2.Domain.Entities
{
    public record HistoryMessage
    {
        public HistoryRole Role;

        public string Message;

        public DateTimeOffset Timestamp;

        public HistoryMessage(HistoryRole role, string message, DateTimeOffset timestamp)
        {
            Role = role;
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Timestamp = timestamp;
        }
    }
}
