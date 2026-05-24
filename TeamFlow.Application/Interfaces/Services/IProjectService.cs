using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Project;

namespace TeamFlow.Application.Interfaces.Services
{
    public interface IProjectService
    {
        Task<ProjectResponseDto> GetByIdAsync(int id);
        Task<IEnumerable<ProjectResponseDto>> GetByTeamIdAsync(int teamId);
        Task<ProjectResponseDto> CreateAsync(CreateProjectDto request);
        Task<ProjectResponseDto> UpdateAsync(int id, UpdateProjectDto request);
        Task DeleteAsync(int id);
    }
}
