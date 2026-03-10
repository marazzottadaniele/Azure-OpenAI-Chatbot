using Microsoft.AspNetCore.Mvc;
using ChatBotV2.Application.Interfaces;
using ChatBotV2.Domain.Entities;

namespace ChatBotV2.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatApplicationService _chatApplicationService;

    public ChatController(IChatApplicationService chatApplicationService)
    {
        _chatApplicationService = chatApplicationService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(object), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<object>> Chat([FromBody] ChatRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var response = await _chatApplicationService.HandleChatRequestAsync(request);
            return Ok(new
            {
                content = response.Content,
                timestamp = response.Timestamp,
                wordCount = response.WordCount
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch
        {
            // Managed by Global Filter Exception
            throw;
        }
    }
}