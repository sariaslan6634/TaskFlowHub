using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Notification;
using TeamFlow.Domain.Entities;

namespace TeamFlow.Application.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationResponseDto>();
            CreateMap<CreateNotificationDto, Notification>();
        }
    }
}
