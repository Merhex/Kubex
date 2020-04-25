using AutoMapper;
using Kubex.DTO.Configurations.Resolvers;
using Kubex.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kubex.DTO.Configurations
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserToReturnDTO>()
                .ForMember(u => u.Roles, opt => opt.MapFrom<RolesResolver>())
                .ReverseMap();

            CreateMap<AddressDTO, Address>()
                .ConvertUsing<AddressConverter>();
            
            CreateMap<Address, AddressDTO>()
                .ConvertUsing<AddressConverter>();

            CreateMap<CompanyDTO, Company>()
                .ReverseMap();

            CreateMap<PostDTO, Post>()
                .ReverseMap();

            CreateMap<LocationDTO, Location>()
                .ReverseMap();

            CreateMap<UserRegisterDTO, User>();
        }
    }
}