using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Comment;

namespace TeamFlow.Application.Interfaces.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentResponseDto>> GetByTaskIdAsync(int taskId);
        Task<CommentResponseDto> CreateAsync(CreateCommentDto request);
        Task DeleteAsync(int id);
    }
}
