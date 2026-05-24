using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFlow.Application.Interfaces.Services
{
    public interface INotificationHubService
    {
        Task SendNotificationAsync(int userId, object notification);
        Task SendMessageAsync(int receiverId, object message);
    }
}
