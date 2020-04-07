using System;
using System.Threading.Tasks;
using Kubex.DAL.Repositories;
using Kubex.DTO;
using Kubex.Models;

namespace Kubex.BLL.Services
{
    public class EntryService : IEntryService
    {
        private readonly IEntryRepository _repository;

        public EntryService(IEntryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Entry> CreateEntryInReport(CreatingEntryDTO dto) 
        {
            if (dto.DailyActivityReport == null)
                throw new ArgumentNullException("The daily activity report can not be empty.");

            if (dto.Priority == null)
                dto.Priority = new Priority { Level = "Normal" };
                
            if (dto.EntryType == null)
                dto.EntryType = new EntryType { Type = "Normal" };

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

            _repository.Add(entry);

            if (! await _repository.SaveAll()) 
            {
                throw new ApplicationException("Something went wrong during the saving of the entry.");
            }

            return entry;
        }
    }
}