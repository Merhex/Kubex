using Kubex.DAL.Repositories.Interfaces;
using Kubex.Models;

namespace Kubex.DAL.Repositories
{
    public class AddressRepository
        : Repository<Address, int>, IAddressRepository
    {
        public AddressRepository(DataContext context)
            : base (context)
        {
            
        }
    }
}