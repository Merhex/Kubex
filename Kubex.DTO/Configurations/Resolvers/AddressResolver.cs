using System;
using System.Threading.Tasks;
using AutoMapper;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.Models;

namespace Kubex.DTO.Configurations.Resolvers
{
    public class AddressConverter : ITypeConverter<AddressDTO, Address>, ITypeConverter<Address, AddressDTO>
    {
        private readonly IAddressRepository _addressRepository;
        public AddressConverter(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public Address Convert(AddressDTO source, Address destination, ResolutionContext context)
        {
            if (source != null)
                return Task.Run(async () => 
                    await _addressRepository.EnsureAsync
                    (
                        new Address 
                        {
                            AppartementBus = source.AppartementBus,
                            HouseNumber = source.HouseNumber,
                            Country = new Country { Name = source.Country },
                            Street = new Street { Name = source.Street },
                            ZIP = new ZIP { Code = source.ZIP }
                        }
                    )
                ).Result;
            throw new ArgumentNullException("The data from the address was invalid, please check the formatting. If you believe this is an error, contact the administrator.");
        }

        public AddressDTO Convert(Address source, AddressDTO destination, ResolutionContext context)
        {
            if (source != null)
                return new AddressDTO 
                { 
                    AppartementBus = source.AppartementBus,
                    HouseNumber = source.HouseNumber,
                    Country = source.Country.Name,
                    Street = source.Street.Name,
                    ZIP = source.ZIP.Code
                };
            return new AddressDTO {};
        }
    }
}