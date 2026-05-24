using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Message;

namespace TeamFlow.Application.Interfaces.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageResponseDto>> GetConversationAsync(int senderId, int receiverId);
        Task<MessageResponseDto> SendAsync(SendMessageDto request);
    }
}
