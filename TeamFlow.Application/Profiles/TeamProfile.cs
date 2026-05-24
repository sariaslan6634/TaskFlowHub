using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Team;
using TeamFlow.Domain.Entities;

namespace TeamFlow.Application.Profiles
{

    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, TeamResponseDto>()
                .ForMember(dest => dest.MemberCount,
                    opt => opt.MapFrom(src => src.Members.Count));

            CreateMap<CreateTeamDto, Team>();
        }
    }
}
