using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Team;
using TeamFlow.Application.Interfaces;
using TeamFlow.Application.Interfaces.Services;
using TeamFlow.Domain.Entities;
using TeamFlow.Domain.Enums;

namespace TeamFlow.Infrastructure.Services
{
    public class TeamService :ITeamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TeamService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TeamResponseDto> GetByIdAsync(int id)
        {
            var team = await _unitOfWork.Teams.GetByIdAsync(id);
            if (team == null)
                throw new KeyNotFoundException($"Takım bulunamadı. Id: {id}");

            return _mapper.Map<TeamResponseDto>(team);
        }

        public async Task<IEnumerable<TeamResponseDto>> GetByUserIdAsync(int userId)
        {
            var teams = await _unitOfWork.Teams.GetTeamsByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<TeamResponseDto>>(teams);
        }

        public async Task<TeamResponseDto> CreateAsync(CreateTeamDto request)
        {
            var team = _mapper.Map<Team>(request);
            await _unitOfWork.Teams.AddAsync(team);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TeamResponseDto>(team);
        }

        public async Task AddMemberAsync(int teamId, int userId)
        {
            var team = await _unitOfWork.Teams.GetByIdAsync(teamId);
            if (team == null)
                throw new KeyNotFoundException($"Takım bulunamadı. Id: {teamId}");

            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"Kullanıcı bulunamadı. Id: {userId}");

            // Kullanıcı zaten takımda mı?
            var existingMember = team.Members?
                .FirstOrDefault(x => x.UserId == userId && !x.IsDeleted);
            if (existingMember != null)
                throw new ArgumentException("Kullanıcı zaten bu takımın üyesi.");

            var teamMember = new TeamMember
            {
                TeamId = teamId,
                UserId = userId,
                RoleInTeam = UserRole.Member,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RemoveMemberAsync(int teamId, int userId)
        {
            var team = await _unitOfWork.Teams.GetByIdAsync(teamId);
            if (team == null)
                throw new KeyNotFoundException($"Takım bulunamadı. Id: {teamId}");

            var member = team.Members?
                .FirstOrDefault(x => x.UserId == userId && !x.IsDeleted);
            if (member == null)
                throw new KeyNotFoundException("Kullanıcı bu takımın üyesi değil.");

            member.IsDeleted = true;
            member.DeletedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var team = await _unitOfWork.Teams.GetByIdAsync(id);
            if (team == null)
                throw new KeyNotFoundException($"Takım bulunamadı. Id: {id}");

            _unitOfWork.Teams.Delete(team);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
