using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Domain.Entities;

namespace TeamFlow.Application.Interfaces.Repositories
{
    public interface INotificationRepository :IGenericRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetUnreadByUserIdAsync(int userId);
        Task MarkAllAsReadAsync(int userId);
    }
}
