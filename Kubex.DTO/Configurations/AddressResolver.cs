using AutoMapper;
using Kubex.Models;

namespace Kubex.DTO.Configurations
{
    public class AddressResolver : IValueResolver<UserRegisterDTO, User, Address>
    {
        public Address Resolve(UserRegisterDTO source, User destination, Address destMember, ResolutionContext context)
        {
            return new Address 
            { 
                HouseNumber = source.HouseNumber,
                AppartementBus = source.AppartementBus,
                Street = new Street { Name = source.Street },
                ZIP = new ZIP { Code = source.ZIP },
                Country = new Country { Name = source.Country }
            };
        }
    }
}