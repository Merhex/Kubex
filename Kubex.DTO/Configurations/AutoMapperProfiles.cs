using System.Collections.Generic;
using AutoMapper;
using Kubex.Models;

namespace Kubex.DTO.Configurations
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserToReturnDTO>()
                .ForMember(u => u.Roles, opt => opt.MapFrom<RolesResolver>())
                .ReverseMap();

            CreateMap<UserRegisterDTO, User>()
                .ForMember(u => u.Address, opt => opt.MapFrom<AddressResolver>());
            
            CreateMap<CreateCompanyDTO, Company>()
                .ReverseMap();
            
            CreateMap<CreatePostDTO, Post>();

            CreateMap<Post, PostToReturnDTO>();
        }
    }
}