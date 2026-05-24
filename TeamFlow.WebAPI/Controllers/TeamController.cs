using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TeamFlow.Application.DTOs.Team;
using TeamFlow.Application.Interfaces.Services;

namespace TeamFlow.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [EnableRateLimiting("GeneralPolicy")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _teamService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var result = await _teamService.GetByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTeamDto request)
        {
            var result = await _teamService.CreateAsync(request);

            // Takımı oluşturan kişiyi üye yap
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim != null)
                await _teamService.AddMemberAsync(result.Id, int.Parse(userIdClaim));

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPost("{teamId}/members/{userId}")]
        public async Task<IActionResult> AddMember(int teamId, int userId)
        {
            await _teamService.AddMemberAsync(teamId, userId);
            return NoContent();
        }

        [HttpDelete("{teamId}/members/{userId}")]
        public async Task<IActionResult> RemoveMember(int teamId, int userId)
        {
            await _teamService.RemoveMemberAsync(teamId, userId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _teamService.DeleteAsync(id);
            return NoContent();
        }
    }
}
