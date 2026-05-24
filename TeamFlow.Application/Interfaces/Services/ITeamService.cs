using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Team;

namespace TeamFlow.Application.Interfaces.Services
{
    public interface ITeamService
    {
        Task<TeamResponseDto> GetByIdAsync(int id);
        Task<IEnumerable<TeamResponseDto>> GetByUserIdAsync(int userId);
        Task<TeamResponseDto> CreateAsync(CreateTeamDto request);
        Task AddMemberAsync(int teamId, int userId);
        Task RemoveMemberAsync(int teamId, int userId);
        Task DeleteAsync(int id);
    }
}
