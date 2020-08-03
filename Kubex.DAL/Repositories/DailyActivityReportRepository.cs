using System.Linq;
using System.Threading.Tasks;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.Models;

namespace Kubex.DAL.Repositories
{
    public class DailyActivityReportRepository 
        : Repository<DailyActivityReport, int>, IDailyActivityReportRepository
    {
        public DailyActivityReportRepository(DataContext context)
            : base(context)
        {
            
        }

        public DailyActivityReport Last()
        {
            return _context.DailyActivityReports.OrderByDescending(x => x.Date).FirstOrDefault();
        }
    }
}