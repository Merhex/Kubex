using System.Threading.Tasks;
using Kubex.DTO;

namespace Kubex.BLL.Services.Interfaces
{
    public interface IDailyActivityReportService
    {
        Task<DailyActivityReportDTO> CreateDailyActivityReportAsync();
        Task<DailyActivityReportDTO> AddEntryAsync(AddEntryToDailyActivityReportDTO dto);
        Task<DailyActivityReportDTO> GetDailyActivityReportAsync(int darId);
        Task DeleteDailyActivityReportAsync(int darId);
        Task DeleteEntryFromDailyActivityReportAsync(int entryId, int darId);
        Task UpdateEntryInDailyActivityReportAsync(AddEntryToDailyActivityReportDTO dto);
    }
}