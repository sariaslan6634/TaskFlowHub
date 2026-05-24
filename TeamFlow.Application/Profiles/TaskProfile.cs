using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Task;
using TeamFlow.Domain.Entities;

namespace TeamFlow.Application.Profiles
{
    public class TaskProfile :Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskItem, TaskResponseDto>()
                .ForMember(dest => dest.AssignedUserFullName,
                    opt => opt.MapFrom(src => src.AssignedUser != null
                        ? $"{src.AssignedUser.FirstName} {src.AssignedUser.LastName}"
                        : null));

            CreateMap<CreateTaskDto, TaskItem>();
            CreateMap<UpdateTaskDto, TaskItem>();
        }
    }
}
