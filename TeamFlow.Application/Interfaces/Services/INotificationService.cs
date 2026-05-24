using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Notification;

namespace TeamFlow.Application.Interfaces.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationResponseDto>> GetUnreadAsync(int userId);
        Task MarkAllAsReadAsync(int userId);
        Task CreateAsync(CreateNotificationDto request);
    }
}
