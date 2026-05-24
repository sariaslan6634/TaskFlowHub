using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Domain.Common;

namespace TeamFlow.Domain.Entities
{
    public class Sprint :BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = false;
        public int ProjectId { get; set; }

        // Navigation
        public Project Project { get; set; } = null!;  // ← Bu satır eksikti
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
