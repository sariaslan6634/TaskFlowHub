using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Sprint;

namespace TeamFlow.Application.Interfaces.Services
{
    public interface ISprintService
    {
        Task<SprintResponseDto> GetByIdAsync(int id);
        Task<IEnumerable<SprintResponseDto>> GetByProjectIdAsync(int projectId);
        Task<SprintResponseDto> CreateAsync(CreateSprintDto request);
        Task<SprintResponseDto> UpdateAsync(int id, UpdateSprintDto request);
        Task DeleteAsync(int id);
        Task<SprintResponseDto> ActivateSprintAsync(int id);
    }
}
