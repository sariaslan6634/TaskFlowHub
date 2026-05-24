using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFlow.Application.DTOs.Team
{
    public class CreateTeamDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
