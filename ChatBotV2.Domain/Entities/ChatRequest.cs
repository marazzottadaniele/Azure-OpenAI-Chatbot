using System.ComponentModel.DataAnnotations;

namespace ChatBotV2.Domain.Entities;

public record ChatRequest(
    [Required(ErrorMessage = "The message is required")]
    [StringLength(2000, ErrorMessage = "The message cannot exceed 2000 characters")]
    string Message
);