using System;
using System.Threading.Tasks;
using Kubex.DTO;
using Kubex.Models;

namespace Kubex.DAL.Repositories
{
    public class EntryRepository : Repository<Entry, int>, IEntryRepository
    {
        public EntryRepository(DataContext context)
            : base(context)
        {

        }

        public Entry Create(CreatingEntryDTO dto)
        {
            var entry = new Entry() 
            {
                OccuranceDate = DateTime.Now,
                Description = dto.Description,                
                ParentEntry = dto.ParentEntry,
                DailyActivityReport = dto.DailyActivityReport,
                EntryType = dto.EntryType,
                Priority = dto.Priority,
                Location = dto.Location,
                ChildEntries = dto.ChildEntries,
                Media = dto.Media,
            };

            Add(entry);

            return entry;
        }
    }
}