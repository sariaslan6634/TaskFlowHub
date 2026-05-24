using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Domain.Enums;
using TaskStatus = TeamFlow.Domain.Enums.TaskStatus;

namespace TeamFlow.Application.DTOs.Task
{
    public class TaskResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public int SprintId { get; set; }
        public int? AssignedUserId { get; set; }
        public string? AssignedUserFullName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
