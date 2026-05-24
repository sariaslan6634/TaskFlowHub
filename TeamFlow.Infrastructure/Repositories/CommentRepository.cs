using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.Interfaces.Repositories;
using TeamFlow.Domain.Entities;
using TeamFlow.Infrastructure.Persistence;

namespace TeamFlow.Infrastructure.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Comment>> GetByTaskItemIdAsync(int taskItemId)
        {
            return await _dbSet
                .Include(x => x.User)
                .Where(x => x.TaskItemId == taskItemId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}
