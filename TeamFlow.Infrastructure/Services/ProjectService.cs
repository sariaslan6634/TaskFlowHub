using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Project;
using TeamFlow.Application.Interfaces;
using TeamFlow.Application.Interfaces.Services;
using TeamFlow.Domain.Entities;

namespace TeamFlow.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProjectResponseDto> GetByIdAsync(int id)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(id);
            if (project == null)
                throw new KeyNotFoundException($"Proje bulunamadı. Id: {id}");

            return _mapper.Map<ProjectResponseDto>(project);
        }

        public async Task<IEnumerable<ProjectResponseDto>> GetByTeamIdAsync(int teamId)
        {
            var projects = await _unitOfWork.Projects.GetByTeamIdAsync(teamId);
            return _mapper.Map<IEnumerable<ProjectResponseDto>>(projects);
        }

        public async Task<ProjectResponseDto> CreateAsync(CreateProjectDto request)
        {
            var team = await _unitOfWork.Teams.GetByIdAsync(request.TeamId);
            if (team == null)
                throw new KeyNotFoundException($"Takım bulunamadı. Id: {request.TeamId}");

            var project = _mapper.Map<Project>(request);
            await _unitOfWork.Projects.AddAsync(project);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProjectResponseDto>(project);
        }

        public async Task<ProjectResponseDto> UpdateAsync(int id, UpdateProjectDto request)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(id);
            if (project == null)
                throw new KeyNotFoundException($"Proje bulunamadı. Id: {id}");

            _mapper.Map(request, project);
            _unitOfWork.Projects.Update(project);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProjectResponseDto>(project);
        }

        public async Task DeleteAsync(int id)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(id);
            if (project == null)
                throw new KeyNotFoundException($"Proje bulunamadı. Id: {id}");

            _unitOfWork.Projects.Delete(project);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
