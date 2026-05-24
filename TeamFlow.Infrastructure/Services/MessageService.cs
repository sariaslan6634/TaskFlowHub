using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Message;
using TeamFlow.Application.Interfaces;
using TeamFlow.Application.Interfaces.Services;
using TeamFlow.Domain.Entities;

namespace TeamFlow.Infrastructure.Services
{
    public class MessageService :IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MessageResponseDto>> GetConversationAsync(
            int senderId, int receiverId)
        {
            var sender = await _unitOfWork.Users.GetByIdAsync(senderId);
            if (sender == null)
                throw new KeyNotFoundException($"Kullanıcı bulunamadı. Id: {senderId}");

            var receiver = await _unitOfWork.Users.GetByIdAsync(receiverId);
            if (receiver == null)
                throw new KeyNotFoundException($"Kullanıcı bulunamadı. Id: {receiverId}");

            var messages = await _unitOfWork.Messages.GetConversationAsync(senderId, receiverId);
            return _mapper.Map<IEnumerable<MessageResponseDto>>(messages);
        }

        public async Task<MessageResponseDto> SendAsync(SendMessageDto request)
        {
            var sender = await _unitOfWork.Users.GetByIdAsync(request.SenderId);
            if (sender == null)
                throw new KeyNotFoundException($"Gönderen bulunamadı. Id: {request.SenderId}");

            var receiver = await _unitOfWork.Users.GetByIdAsync(request.ReceiverId);
            if (receiver == null)
                throw new KeyNotFoundException($"Alıcı bulunamadı. Id: {request.ReceiverId}");

            var message = _mapper.Map<Message>(request);
            await _unitOfWork.Messages.AddAsync(message);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<MessageResponseDto>(message);
        }
    }
}
