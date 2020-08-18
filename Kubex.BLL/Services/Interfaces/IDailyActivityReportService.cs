using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kubex.DTO;

namespace Kubex.BLL.Services.Interfaces
{
    public interface IDailyActivityReportService
    {
        Task<DailyActivityReportDTO> CreateDailyActivityReportAsync(int postId);
        Task<DailyActivityReportDTO> AddEntryAsync(AddEntryToDailyActivityReportDTO dto);
        Task<DailyActivityReportDTO> GetDailyActivityReportAsync(int darId);
        Task<DailyActivityReportDTO> GetLastDailyActivityReportAsync(int postId);
        Task DeleteDailyActivityReportAsync(int darId);
        Task DeleteEntryFromDailyActivityReportAsync(int entryId, int darId);
        Task UpdateEntryInDailyActivityReportAsync(AddEntryToDailyActivityReportDTO dto);
        Task<IEnumerable<DailyActivityReportDTO>> GetDailyActivityReportForPostAsync(int postId);
    }
}