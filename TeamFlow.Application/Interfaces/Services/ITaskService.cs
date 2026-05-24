using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.Common;
using TeamFlow.Application.DTOs.Task;

namespace TeamFlow.Application.Interfaces.Services
{
    public interface ITaskService
    {
        Task<TaskResponseDto> GetByIdAsync(int id);
        Task<IEnumerable<TaskResponseDto>> GetBySprintIdAsync(int sprintId);
        Task<PaginatedResult<TaskResponseDto>> GetPagedAsync(
            int sprintId, PaginationParams pagination); // ← ekle
        Task<TaskResponseDto> CreateAsync(CreateTaskDto request);
        Task<TaskResponseDto> UpdateAsync(int id, UpdateTaskDto request);
        Task DeleteAsync(int id);
        Task<TaskResponseDto> ChangeStatusAsync(int id, ChangeTaskStatusDto request);
        Task<TaskResponseDto> AssignUserAsync(int id, int userId);
        Task<IEnumerable<TaskResponseDto>> GetByAssignedUserAsync(int userId);
    }
}
