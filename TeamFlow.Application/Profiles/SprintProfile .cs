using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Sprint;
using TeamFlow.Domain.Entities;

namespace TeamFlow.Application.Profiles
{
    public class SprintProfile : Profile
    {
        public SprintProfile()
        {
            CreateMap<Sprint, SprintResponseDto>();
            CreateMap<CreateSprintDto, Sprint>();
            CreateMap<UpdateSprintDto, Sprint>();
        }
    }
}
