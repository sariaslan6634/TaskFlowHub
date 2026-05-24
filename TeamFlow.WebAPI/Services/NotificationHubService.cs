using Microsoft.AspNetCore.SignalR;
using TeamFlow.Application.Interfaces.Services;
using TeamFlow.WebAPI.Hubs;

namespace TeamFlow.WebAPI.Services
{
    public class NotificationHubService : INotificationHubService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IHubContext<ChatHub> _chatHubContext;

        public NotificationHubService(
            IHubContext<NotificationHub> hubContext,
            IHubContext<ChatHub> chatHubContext)
        {
            _hubContext = hubContext;
            _chatHubContext = chatHubContext;
        }

        public async Task SendNotificationAsync(int userId, object notification)
        {
            await _hubContext.Clients
                .Group($"user_{userId}")
                .SendAsync("ReceiveNotification", notification);
        }

        public async Task SendMessageAsync(int receiverId, object message)
        {
            await _chatHubContext.Clients
                .Group($"user_{receiverId}")
                .SendAsync("ReceiveMessage", message);
        }
    }
}
