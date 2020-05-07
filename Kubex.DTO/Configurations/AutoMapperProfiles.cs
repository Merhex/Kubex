using AutoMapper;
using Kubex.DTO.Configurations.Resolvers;
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
            
            CreateMap<string, EntryType>()
                .ReverseMap()
                .ConvertUsing<EntryTypeConverter>();

            CreateMap<string, Priority>()
                .ReverseMap()
                .ConvertUsing<PriorityConverter>();

            CreateMap<string, MediaType>()
                .ReverseMap()
                .ConvertUsing<MediaTypeConverter>();
                
            CreateMap<DailyActivityReportDTO, DailyActivityReport>()
                .ReverseMap();

            CreateMap<Entry, EntryDTO>()
                .ForMember(x => x.EntryType, opt => opt.MapFrom<EntryTypeConverter>())
                .ForMember(x => x.Priority, opt => opt.MapFrom<PriorityConverter>());
            
            CreateMap<EntryDTO, Entry>()
                .ForMember(x => x.EntryType, opt => opt.MapFrom<EntryTypeConverter>())
                .ForMember(x => x.Priority, opt => opt.MapFrom<PriorityConverter>());

        }
    }
}