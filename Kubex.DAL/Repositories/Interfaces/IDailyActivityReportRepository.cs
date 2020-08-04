using System.Threading.Tasks;
using Kubex.Models;

namespace Kubex.DAL.Repositories.Interfaces
{
    public interface IDailyActivityReportRepository : IRepository<DailyActivityReport, int>
    {
         DailyActivityReport Last();
    }
}