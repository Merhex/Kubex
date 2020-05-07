using System.Threading.Tasks;
using Kubex.Models;

namespace Kubex.DAL.Repositories.Interfaces
{
    public interface IMediaTypeRepository
    {
         Task<MediaType> FindByType(string type);
    }
}