using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.Interfaces.Repositories;

namespace TeamFlow.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ITaskRepository Tasks { get; }
        IProjectRepository Projects { get; }
        ISprintRepository Sprints { get; }
        ITeamRepository Teams { get; }
        INotificationRepository Notifications { get; }
        IMessageRepository Messages { get; }
        IAuditLogRepository AuditLogs { get; }

        Task<int> SaveChangesAsync();
    }
}
