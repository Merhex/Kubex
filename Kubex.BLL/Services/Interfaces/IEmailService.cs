using System.Threading.Tasks;

namespace Kubex.BLL.Services.Interfaces
{
    public interface IEmailService
    {
         Task SendReport(int compantId);
    }
}