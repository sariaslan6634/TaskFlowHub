using AutoMapper;
using TeamFlow.Application.Common;
using TeamFlow.Application.DTOs.Task;
using TeamFlow.Application.Interfaces;
using TeamFlow.Application.Interfaces.Services;
using TeamFlow.Domain.Entities;
using TaskStatus = TeamFlow.Domain.Enums.TaskStatus;

namespace TeamFlow.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TaskService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TaskResponseDto> GetByIdAsync(int id)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);
            if (task == null)
                throw new KeyNotFoundException($"Görev bulunamadı. Id: {id}");

            return _mapper.Map<TaskResponseDto>(task);
        }

        public async Task<IEnumerable<TaskResponseDto>> GetBySprintIdAsync(int sprintId)
        {
            var tasks = await _unitOfWork.Tasks.GetBySprintIdAsync(sprintId);
            return _mapper.Map<IEnumerable<TaskResponseDto>>(tasks);
        }

        public async Task<TaskResponseDto> CreateAsync(CreateTaskDto request)
        {
            {
                var sprint = await _unitOfWork.Sprints.GetByIdAsync(request.SprintId);
                if (sprint == null)
                    throw new KeyNotFoundException($"Sprint bulunamadı. Id: {request.SprintId}");

                if (request.AssignedUserId.HasValue)
                {
                    var user = await _unitOfWork.Users.GetByIdAsync(request.AssignedUserId.Value);
                    if (user == null)
                        throw new KeyNotFoundException($"Kullanıcı bulunamadı. Id: {request.AssignedUserId}");
                }

                var task = _mapper.Map<TaskItem>(request);
                await _unitOfWork.Tasks.AddAsync(task);

                // Sadece kullanıcı atandıysa audit log yaz
                if (request.AssignedUserId.HasValue && request.AssignedUserId.Value > 0)
                {
                    await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                    {
                        EntityName = "TaskItem",
                        Action = "Created",
                        NewValue = task.Title,
                        UserId = request.AssignedUserId.Value,
                        TaskItemId = task.Id
                    });
                }

                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<TaskResponseDto>(task);
            }
        }

        public async Task<TaskResponseDto> UpdateAsync(int id, UpdateTaskDto request)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);
            if (task == null)
                throw new KeyNotFoundException($"Görev bulunamadı. Id: {id}");

            // Sadece değişen alanları güncelle
            _mapper.Map(request, task);
            _unitOfWork.Tasks.Update(task);

            await _unitOfWork.AuditLogs.AddAsync(new AuditLog
            {
                EntityName = "TaskItem",
                Action = "Updated",
                NewValue = task.Title,
                UserId = task.AssignedUserId ?? 0,
                TaskItemId = task.Id
            });

            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TaskResponseDto>(task);
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);
            if (task == null)
                throw new KeyNotFoundException($"Görev bulunamadı. Id: {id}");

            _unitOfWork.Tasks.Delete(task); // Soft delete
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<TaskResponseDto> ChangeStatusAsync(int id, ChangeTaskStatusDto request)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);
            if (task == null)
                throw new KeyNotFoundException($"Görev bulunamadı. Id: {id}");

            var oldStatus = task.Status;
            task.Status = request.NewStatus;
            _unitOfWork.Tasks.Update(task);

            // Durum değişikliğini audit log'a yaz
            await _unitOfWork.AuditLogs.AddAsync(new AuditLog
            {
                EntityName = "TaskItem",
                Action = "StatusChanged",
                OldValue = oldStatus.ToString(),
                NewValue = request.NewStatus.ToString(),
                UserId = task.AssignedUserId ?? 0,
                TaskItemId = task.Id
            });

            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TaskResponseDto>(task);
        }

        public async Task<TaskResponseDto> AssignUserAsync(int id, int userId)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);
            if (task == null)
                throw new KeyNotFoundException($"Görev bulunamadı. Id: {id}");

            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"Kullanıcı bulunamadı. Id: {userId}");

            var oldUserId = task.AssignedUserId;
            task.AssignedUserId = userId;
            _unitOfWork.Tasks.Update(task);

            await _unitOfWork.AuditLogs.AddAsync(new AuditLog
            {
                EntityName = "TaskItem",
                Action = "UserAssigned",
                OldValue = oldUserId?.ToString(),
                NewValue = userId.ToString(),
                UserId = userId,
                TaskItemId = task.Id
            });

            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TaskResponseDto>(task);
        }

        public async Task<PaginatedResult<TaskResponseDto>> GetPagedAsync(int sprintId, PaginationParams pagination)
        {
            var sprint = await _unitOfWork.Sprints.GetByIdAsync(sprintId);
            if (sprint == null)
                throw new KeyNotFoundException($"Sprint bulunamadı. Id: {sprintId}");

            var tasks = await _unitOfWork.Tasks.GetBySprintIdAsync(sprintId);

            var totalCount = tasks.Count();

            var pagedTasks = tasks
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToList();

            return new PaginatedResult<TaskResponseDto>
            {
                Items = _mapper.Map<IEnumerable<TaskResponseDto>>(pagedTasks),
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }

        public async Task<IEnumerable<TaskResponseDto>> GetByAssignedUserAsync(int userId)
        {
            var tasks = await _unitOfWork.Tasks.GetByAssignedUserIdAsync(userId);
            return _mapper.Map<IEnumerable<TaskResponseDto>>(tasks);
        }
    }
}
