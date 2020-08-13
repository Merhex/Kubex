using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Kubex.BLL.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadImage(IFormFile file);
    }
}