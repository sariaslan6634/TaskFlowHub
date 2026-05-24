using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFlow.Domain.Enums
{
    public enum NotificationType
    {
        TaskAssigned,
        TaskStatusChanged,
        CommentAdded,
        MentionedInComment,
        SprintStarted,
        SprintCompleted
    }
}
