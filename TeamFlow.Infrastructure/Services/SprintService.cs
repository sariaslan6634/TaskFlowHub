using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Sprint;
using TeamFlow.Application.Interfaces;
using TeamFlow.Application.Interfaces.Services;
using TeamFlow.Domain.Entities;

namespace TeamFlow.Infrastructure.Services
{
    public class SprintService :ISprintService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SprintService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SprintResponseDto> GetByIdAsync(int id)
        {
            var sprint = await _unitOfWork.Sprints.GetByIdAsync(id);
            if (sprint == null)
                throw new KeyNotFoundException($"Sprint bulunamadı. Id: {id}");

            return _mapper.Map<SprintResponseDto>(sprint);
        }

        public async Task<IEnumerable<SprintResponseDto>> GetByProjectIdAsync(int projectId)
        {
            var sprints = await _unitOfWork.Sprints.GetByProjectIdAsync(projectId);
            return _mapper.Map<IEnumerable<SprintResponseDto>>(sprints);
        }

        public async Task<SprintResponseDto> CreateAsync(CreateSprintDto request)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(request.ProjectId);
            if (project == null)
                throw new KeyNotFoundException($"Proje bulunamadı. Id: {request.ProjectId}");

            // Bitiş tarihi başlangıçtan önce olamaz
            if (request.EndDate <= request.StartDate)
                throw new ArgumentException("Bitiş tarihi başlangıç tarihinden sonra olmalıdır.");

            var sprint = _mapper.Map<Sprint>(request);
            await _unitOfWork.Sprints.AddAsync(sprint);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SprintResponseDto>(sprint);
        }

        public async Task<SprintResponseDto> UpdateAsync(int id, UpdateSprintDto request)
        {
            var sprint = await _unitOfWork.Sprints.GetByIdAsync(id);
            if (sprint == null)
                throw new KeyNotFoundException($"Sprint bulunamadı. Id: {id}");

            if (request.EndDate <= request.StartDate)
                throw new ArgumentException("Bitiş tarihi başlangıç tarihinden sonra olmalıdır.");

            _mapper.Map(request, sprint);
            _unitOfWork.Sprints.Update(sprint);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SprintResponseDto>(sprint);
        }

        public async Task DeleteAsync(int id)
        {
            var sprint = await _unitOfWork.Sprints.GetByIdAsync(id);
            if (sprint == null)
                throw new KeyNotFoundException($"Sprint bulunamadı. Id: {id}");

            _unitOfWork.Sprints.Delete(sprint);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<SprintResponseDto> ActivateSprintAsync(int id)
        {
            var sprint = await _unitOfWork.Sprints.GetByIdAsync(id);
            if (sprint == null)
                throw new KeyNotFoundException($"Sprint bulunamadı. Id: {id}");

            // Önce aynı projedeki aktif sprint'i kapat
            var activeSprint = await _unitOfWork.Sprints.GetActiveSprintAsync(sprint.ProjectId);
            if (activeSprint != null && activeSprint.Id != id)
            {
                activeSprint.IsActive = false;
                _unitOfWork.Sprints.Update(activeSprint);
            }

            sprint.IsActive = true;
            _unitOfWork.Sprints.Update(sprint);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SprintResponseDto>(sprint);
        }
    }
}
