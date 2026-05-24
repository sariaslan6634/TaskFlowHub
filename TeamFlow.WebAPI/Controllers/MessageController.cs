using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TeamFlow.Application.DTOs.Message;
using TeamFlow.Application.Interfaces.Services;

namespace TeamFlow.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableRateLimiting("GeneralPolicy")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("conversation/{senderId}/{receiverId}")]
        public async Task<IActionResult> GetConversation(int senderId, int receiverId)
        {
            var result = await _messageService.GetConversationAsync(senderId, receiverId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] SendMessageDto request)
        {
            var result = await _messageService.SendAsync(request);
            return Ok(result);
        }
    }
}
