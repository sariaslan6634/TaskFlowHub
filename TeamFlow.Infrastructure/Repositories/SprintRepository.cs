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
    public class SprintRepository : GenericRepository<Sprint>, ISprintRepository
    {
        public SprintRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Sprint?> GetActiveSprintAsync(int projectId)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.ProjectId == projectId && !x.IsActive);
        }

        public async Task<IEnumerable<Sprint>> GetByProjectIdAsync(int projectId)
        {
            return await _dbSet
                .Where(x => x.ProjectId == projectId)
                .ToListAsync();
        }
    }
}
