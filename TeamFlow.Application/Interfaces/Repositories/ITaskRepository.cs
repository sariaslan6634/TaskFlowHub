using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Domain.Entities;
using TaskStatus = TeamFlow.Domain.Enums.TaskStatus;
namespace TeamFlow.Application.Interfaces.Repositories
{
    public interface ITaskRepository:IGenericRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetBySprintIdAsync(int sprintId);
        Task<IEnumerable<TaskItem>> GetByAssignedUserIdAsync(int userId);
        Task<IEnumerable<TaskItem>> GetByStatusAsync(TaskStatus status);
    }
}
