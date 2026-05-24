using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.Interfaces;
using TeamFlow.Application.Interfaces.Repositories;
using TeamFlow.Infrastructure.Persistence;

namespace TeamFlow.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IUserRepository Users { get; }
        public ITaskRepository Tasks { get; }
        public IProjectRepository Projects { get; }
        public ISprintRepository Sprints { get; }
        public ITeamRepository Teams { get; }
        public INotificationRepository Notifications { get; }
        public IMessageRepository Messages { get; }
        public IAuditLogRepository AuditLogs { get; }
        public ICommentRepository Comments { get; }


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new UserRepository(context);
            Tasks = new TaskRepository(context);
            Projects = new ProjectRepository(context);
            Sprints = new SprintRepository(context);
            Teams = new TeamRepository(context);
            Notifications = new NotificationRepository(context);
            Messages = new MessageRepository(context);
            AuditLogs = new AuditLogRepository(context);
        }



        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
