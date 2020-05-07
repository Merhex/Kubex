using System.Linq;
using System.Threading.Tasks;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.Models;

namespace Kubex.DAL.Repositories
{
    public class AddressRepository
        : Repository<Address, int>, IAddressRepository
    {
        private readonly IStreetRepository _streetRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IZIPCodeRepository _zipCodeRepository;

        public AddressRepository(DataContext context,
        IStreetRepository streetRepository,
        ICountryRepository countryRepository,
        IZIPCodeRepository zipCodeRepository)
            : base(context)
        {
            _countryRepository = countryRepository;
            _zipCodeRepository = zipCodeRepository;
            _streetRepository = streetRepository;
        }

        public async Task<Address> ExistsAsync(Address address) 
        {
            var country =  await _countryRepository.FindByNameAsync(address.Country.Name);
            var ZIP =  await _zipCodeRepository.FindByCodeAsync(address.ZIP.Code);
            var street = await _streetRepository.FindByNameAsync(address.Street.Name);

            if (country == null || ZIP == null || street == null)
                return null;

            var result = await FindRange
            (
                x =>

                x.AppartementBus == address.AppartementBus &&
                x.Country.Name == country.Name &&
                x.ZIP.Code == ZIP.Code &&
                x.Street.Name == street.Name &&
                x.HouseNumber == address.HouseNumber
            );       
            return result.FirstOrDefault();
        }

        public async Task<Address> EnsureAsync(Address address) 
        {
            var existingAddress = await ExistsAsync(address);

            if (existingAddress != null)
                return existingAddress;

            var country =  await _countryRepository.FindByNameAsync(address.Country.Name);
            var ZIP =  await _zipCodeRepository.FindByCodeAsync(address.ZIP.Code);
            var street = await _streetRepository.FindByNameAsync(address.Street.Name);

            if (country == null) 
            {
                _countryRepository.Add(address.Country);
                await _countryRepository.SaveAll();
            }

            if (ZIP == null) 
            {
                _zipCodeRepository.Add(address.ZIP);
                await _zipCodeRepository.SaveAll();
            }

            if (street == null) 
            {
                _streetRepository.Add(address.Street);
                await _streetRepository.SaveAll();
            }

            address.Country = await _countryRepository.FindByNameAsync(address.Country.Name);
            address.ZIP = await _zipCodeRepository.FindByCodeAsync(address.ZIP.Code);
            address.Street = await _streetRepository.FindByNameAsync(address.Street.Name);

            Add(address);
            await SaveAll();

            return address;
        }
    }
}