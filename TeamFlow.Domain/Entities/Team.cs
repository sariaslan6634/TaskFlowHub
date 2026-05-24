using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Domain.Common;

namespace TeamFlow.Domain.Entities
{
    public class Team:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        // Navigation
        public ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
