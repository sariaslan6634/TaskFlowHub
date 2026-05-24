using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Message;
using TeamFlow.Application.DTOs.Notification;
using TeamFlow.Domain.Entities;

namespace TeamFlow.Application.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageResponseDto>()
                .ForMember(dest => dest.SenderFullName,
                    opt => opt.MapFrom(src =>
                        $"{src.Sender.FirstName} {src.Sender.LastName}"));

            CreateMap<SendMessageDto, Message>();
        }
    }
}
