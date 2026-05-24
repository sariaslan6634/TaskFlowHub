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
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Team>> GetTeamsByUserIdAsync(int userId)
        {
            return await _context.TeamMembers
                .Where(x => x.UserId == userId)
                .Select(x => x.Team)
                .ToListAsync();
        }
    }
}
