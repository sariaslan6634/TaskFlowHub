using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Comment;
using TeamFlow.Application.Interfaces;
using TeamFlow.Application.Interfaces.Services;
using TeamFlow.Domain.Entities;

namespace TeamFlow.Infrastructure.Services
{
    public class CommentService :ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentResponseDto>> GetByTaskIdAsync(int taskId)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(taskId);
            if (task == null)
                throw new KeyNotFoundException($"Görev bulunamadı. Id: {taskId}");

            var comments = await _unitOfWork.Comments.GetByTaskItemIdAsync(taskId);
            return _mapper.Map<IEnumerable<CommentResponseDto>>(comments);
        }

        public async Task<CommentResponseDto> CreateAsync(CreateCommentDto request)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(request.TaskItemId);
            if (task == null)
                throw new KeyNotFoundException($"Görev bulunamadı. Id: {request.TaskItemId}");

            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
            if (user == null)
                throw new KeyNotFoundException($"Kullanıcı bulunamadı. Id: {request.UserId}");

            var comment = _mapper.Map<Comment>(request);
            await _unitOfWork.Comments.AddAsync(comment);

            // Audit log
            await _unitOfWork.AuditLogs.AddAsync(new AuditLog
            {
                EntityName = "Comment",
                Action = "Created",
                NewValue = comment.Content,
                UserId = request.UserId,
                TaskItemId = request.TaskItemId
            });

            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CommentResponseDto>(comment);
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(id);
            if (comment == null)
                throw new KeyNotFoundException($"Yorum bulunamadı. Id: {id}");

            _unitOfWork.Comments.Delete(comment);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
