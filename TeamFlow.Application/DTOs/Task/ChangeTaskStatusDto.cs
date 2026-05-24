using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = TeamFlow.Domain.Enums.TaskStatus;

namespace TeamFlow.Application.DTOs.Task
{
    public class ChangeTaskStatusDto
    {
        public TaskStatus NewStatus { get; set; }
    }
}
