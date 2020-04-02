using AutoMapper;
using Kubex.Models;

namespace Kubex.DTO.Configurations
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserToReturnDTO>();

            CreateMap<UserToReturnDTO, User>();

            CreateMap<UserRegisterDTO, User>()
                .ForMember(u => u.Address, opt => opt.MapFrom<AddressResolver>());
        }
    }
}