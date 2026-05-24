using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Domain.Common;

namespace TeamFlow.Domain.Entities
{
    public class Project :BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int TeamId { get; set; }

        // Navigation
        public Team Team { get; set; } = null!;
        public ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();
    }
}
