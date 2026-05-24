
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Domain.Entities;

namespace TeamFlow.Application.Interfaces.Repositories
{
    public interface ISprintRepository : IGenericRepository<Sprint>
    {
        Task<IEnumerable<Sprint>> GetByProjectIdAsync(int projectId);
        Task<Sprint?> GetActiveSprintAsync(int projectId);
    }
}
