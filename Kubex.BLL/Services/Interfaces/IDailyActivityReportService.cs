using System;
using System.Threading.Tasks;
using Kubex.DTO;

namespace Kubex.BLL.Services.Interfaces
{
    public interface IDailyActivityReportService
    {
        Task<DailyActivityReportDTO> CreateDailyActivityReportAsync();
        Task<DailyActivityReportDTO> AddEntryAsync(AddEntryToDailyActivityReportDTO dto);
        Task<DailyActivityReportDTO> GetDailyActivityReportAsync(int id);
        Task<DailyActivityReportDTO> GetLastDailyActivityReportAsync();
        Task DeleteDailyActivityReportAsync(int id);
        Task DeleteEntryFromDailyActivityReportAsync(int entryId, int darId);
        Task<DailyActivityReportDTO> AddChildEntryAsync(AddEntryToDailyActivityReportDTO dto);

    }
}