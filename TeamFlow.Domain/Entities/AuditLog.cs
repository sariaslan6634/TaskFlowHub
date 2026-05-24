using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Domain.Common;

namespace TeamFlow.Domain.Entities
{
    public class AuditLog:BaseEntity
    {
        public string EntityName { get; set; } = string.Empty;  // "TaskItem"
        public string Action { get; set; } = string.Empty;      // "StatusChanged"
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public int UserId { get; set; }
        public int? TaskItemId { get; set; }

        // Navigation
        public User User { get; set; } = null!;
        public TaskItem? TaskItem { get; set; }
    }
}
