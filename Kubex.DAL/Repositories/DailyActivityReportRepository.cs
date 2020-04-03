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
    }
}