using Microsoft.AspNetCore.SignalR;

namespace TeamFlow.WebAPI.Hubs
{
    public class ChatHub :Hub
    { 
        // Mesaj gönder
        public async Task SendMessage(int receiverId, string message)
        {
            var senderId = Context.UserIdentifier;

            // Sadece alıcıya gönder
            await Clients.Group($"user_{receiverId}")
                .SendAsync("ReceiveMessage", new
                {
                    SenderId = senderId,
                    Message = message,
                    SentAt = DateTime.UtcNow
                });
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            if (userId != null)
                await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;
            if (userId != null)
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userId}");

            await base.OnDisconnectedAsync(exception);
        }
    }
}
