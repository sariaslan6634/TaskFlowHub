using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Project;
using TeamFlow.Domain.Entities;

namespace TeamFlow.Application.Profiles
{
    public class ProjectProfile :Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectResponseDto>();
            CreateMap<CreateProjectDto, Project>();
            CreateMap<UpdateProjectDto, Project>();
        }
    }
}
