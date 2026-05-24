using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TeamFlow.Domain.Common;
using TeamFlow.Domain.Enums;
using TaskStatus = TeamFlow.Domain.Enums.TaskStatus;

namespace TeamFlow.Domain.Entities
{
    public class TaskItem :BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.Todo;
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public DateTime? DueDate { get; set; }
        public int SprintId { get; set; }
        public int? AssignedUserId { get; set; }
        public byte[] RowVersion { get; set; } = null!; // Optimistic Concurrency

        // Navigation
        public Sprint Sprint { get; set; } = null!;
        public User? AssignedUser { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    }
}
