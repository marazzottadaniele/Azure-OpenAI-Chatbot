using Microsoft.AspNetCore.Mvc;
using ChatBotV2.Application.Interfaces;
using ChatBotV2.Domain.Entities;

namespace ChatBotV2.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SettingsController : ControllerBase
{
    private readonly ISettingsService _settingsService;

    public SettingsController(ISettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ChatbotSettings), 200)]
    public ActionResult<ChatbotSettings> GetSettings()
    {
        var settings = _settingsService.GetSettings();
        return Ok(settings);
    }

    [HttpPut]
    [ProducesResponseType(typeof(ChatbotSettings), 200)]
    [ProducesResponseType(400)]
    public ActionResult<ChatbotSettings> UpdateSettings([FromBody] ChatbotSettings settings)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _settingsService.UpdateSettings(settings);
        return Ok(_settingsService.GetSettings());
    }
}