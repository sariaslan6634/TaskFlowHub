using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TeamFlow.Application.Common;
using TeamFlow.Application.DTOs.Task;
using TeamFlow.Application.Interfaces.Services;

namespace TeamFlow.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableRateLimiting("GeneralPolicy")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("sprint/{sprintId}/paged")]
        public async Task<IActionResult> GetPaged(int sprintId, [FromQuery] PaginationParams pagination)
        {
            var result = await _taskService.GetPagedAsync(sprintId, pagination);
            return Ok(result);
        }
        [HttpGet("assigned/{userId}")]
        public async Task<IActionResult> GetByAssignedUser(int userId)
        {
            var result = await _taskService.GetByAssignedUserAsync(userId);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _taskService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("sprint/{sprintId}")]
        public async Task<IActionResult> GetBySprintId(int sprintId)
        {
            var result = await _taskService.GetBySprintIdAsync(sprintId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto request)
        {
            var result = await _taskService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskDto request)
        {
            var result = await _taskService.UpdateAsync(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] ChangeTaskStatusDto request)
        {
            var result = await _taskService.ChangeStatusAsync(id, request);
            return Ok(result);
        }

        [HttpPatch("{id}/assign/{userId}")]
        public async Task<IActionResult> AssignUser(int id, int userId)
        {
            var result = await _taskService.AssignUserAsync(id, userId);
            return Ok(result);
        }
    }
}
