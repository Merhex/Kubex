using System.Threading.Tasks;
using Kubex.DTO;

namespace Kubex.BLL.Services.Interfaces
{
    public interface IDailyActivityReportService
    {
        Task<DailyActivityReportDTO> CreateDailyActivityReportAsync();
        Task<DailyActivityReportDTO> AddEntryToDailyActivityReportAsync(AddEntryToDailyActivityReportDTO dto);
        Task<DailyActivityReportDTO> GetDailyActivityReportAsync(int id);
        Task DeleteDailyActivityReportAsync(int id);
    }
}