using System.Collections.Generic;
using System.Threading.Tasks;
using Kubex.Models;

namespace Kubex.DAL.Repositories.Interfaces
{
    public interface IEntryRepository : IRepository<Entry, int>
    {
        Task<IEnumerable<Entry>> GetEntriesForDailyActivityReportAsync(int darId);
        Task<IEnumerable<Entry>> GetChildEntriesAsync(int entryId);
    }
}