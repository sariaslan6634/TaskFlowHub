using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Domain.Common;

namespace TeamFlow.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public int TaskItemId { get; set; }
        public int UserId { get; set; }

        // Navigation
        public TaskItem TaskItem { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
