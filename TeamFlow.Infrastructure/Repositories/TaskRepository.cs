using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.Interfaces.Repositories;
using TeamFlow.Domain.Entities;
using TeamFlow.Infrastructure.Persistence;
using TaskStatus = TeamFlow.Domain.Enums.TaskStatus;

namespace TeamFlow.Infrastructure.Repositories
{
    internal class TaskRepository : GenericRepository<TaskItem>, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TaskItem>> GetByAssignedUserIdAsync(int userId)
        {
            return await _dbSet
            .Include(x => x.Sprint)
            .Where(x => x.AssignedUserId == userId)
            .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetBySprintIdAsync(int sprintId)
        {
            return await _dbSet
            .Include(x => x.AssignedUser)
            .Where(x => x.SprintId == sprintId)
            .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetByStatusAsync(TaskStatus status)
        {
            return await _dbSet
            .Include(x => x.AssignedUser)
            .Where(x => x.Status == status)
            .ToListAsync();
        }
    }
}
