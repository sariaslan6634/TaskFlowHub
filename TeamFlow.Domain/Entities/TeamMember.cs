using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Domain.Common;
using TeamFlow.Domain.Enums;

namespace TeamFlow.Domain.Entities
{
    public class TeamMember :BaseEntity
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public UserRole RoleInTeam { get; set; }
        public User User { get; set; } = null!;
        public Team Team { get; set; } = null!;
    }
}
