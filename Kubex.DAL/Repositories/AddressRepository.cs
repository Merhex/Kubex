using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
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