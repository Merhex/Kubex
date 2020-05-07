using System.Threading.Tasks;
using Kubex.Models;

namespace Kubex.DAL.Repositories.Interfaces
{
    public interface IAddressRepository : IRepository<Address, int>
    {
        Task<Address> EnsureAsync(Address address);
        Task<Address> ExistsAsync(Address address);
    }
}