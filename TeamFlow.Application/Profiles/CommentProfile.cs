using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Comment;
using TeamFlow.Domain.Entities;

namespace TeamFlow.Application.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentResponseDto>()
                .ForMember(dest => dest.UserFullName,
                    opt => opt.MapFrom(src =>
                        $"{src.User.FirstName} {src.User.LastName}"));

            CreateMap<CreateCommentDto, Comment>();
        }
    }
}
