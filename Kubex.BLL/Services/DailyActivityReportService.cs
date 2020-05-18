using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Kubex.BLL.Services.Interfaces;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.DTO;
using Kubex.Models;

namespace Kubex.BLL.Services
{
    public class DailyActivityReportService : IDailyActivityReportService
    {
        private readonly IMapper _mapper;
        private readonly IDailyActivityReportRepository _darRepository;
        private readonly IEntryRepository _entryRepository;

        public DailyActivityReportService(IMapper mapper,
            IDailyActivityReportRepository darRepository,
            IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
            _mapper = mapper;
            _darRepository = darRepository;
        }

        public async Task<DailyActivityReportDTO> CreateDailyActivityReportAsync()
        {
            var report = new DailyActivityReport { Date = DateTime.Now, Entries = new List<Entry>() };

            _darRepository.Add(report);

            if (await _darRepository.SaveAll())
            {
                var reportToReturn = _mapper.Map<DailyActivityReportDTO>(report);

                return reportToReturn;
            }

            throw new ApplicationException("Something went wrong creating a new report.");
        }

        public async Task<DailyActivityReportDTO> AddEntryAsync(AddEntryToDailyActivityReportDTO dto)
        {
            if (dto.ParentEntry != null)
                return await AddChildEntryAsync(dto);
        
            var entry = _mapper.Map<Entry>(dto.Entry);
            var dar = await FindDailyActivityReport(dto.DailyActivityReport.Id);

            return await AddEntryToDailyActivityReportAndUpdate(entry, dar);
        }

        private async Task<DailyActivityReportDTO> AddChildEntryAsync(AddEntryToDailyActivityReportDTO dto) 
        {
            var parentEntry = await FindEntry(dto.ParentEntry.Id);
            var dar = await FindDailyActivityReport(dto.DailyActivityReport.Id);

            if (parentEntry.DailyActivityReportId != dar.Id)
                throw new ApplicationException("This parent entry does not belong to this Daily Activity Report.");
            
            var childEntry = _mapper.Map<Entry>(dto.Entry);

            return await AddEntryToDailyActivityReportAndUpdate(childEntry, dar, parentEntry);
        }

        private async Task<DailyActivityReportDTO> AddEntryToDailyActivityReportAndUpdate(Entry entry, DailyActivityReport dar, Entry parent = null) 
        {
            entry.ParentEntry = parent;
            entry.DailyActivityReport = dar;

            if (entry.OccuranceDate == null)
                entry.OccuranceDate = DateTime.Now;

            if (parent == null) 
                dar.Entries.Add(entry);
            else 
                parent.ChildEntries.Add(entry);

            _entryRepository.Add(entry);

            if (! await _entryRepository.SaveAll())
                throw new ApplicationException("Something went wrong saving the entry to the database.");

            var entries = await _entryRepository
                .FindRange(x => x.ParentEntry == null && x.DailyActivityReportId == dar.Id);
            
            var darToReturn = _mapper.Map<DailyActivityReportDTO>(dar);
            var darEntries = _mapper.Map<ICollection<EntryDTO>>(entries);

            darToReturn.Entries = darEntries;
            
            return darToReturn;
        }

        public async Task<DailyActivityReportDTO> GetDailyActivityReportAsync(int darId)
        {
            var dar = await FindDailyActivityReport(darId);

            var entries =  await _entryRepository
                .FindRange(x => x.ParentEntry == null && x.DailyActivityReportId == dar.Id);
            
            var darToReturn = _mapper.Map<DailyActivityReportDTO>(dar);
            var darEntries = _mapper.Map<ICollection<EntryDTO>>(entries);

            darToReturn.Entries = darEntries;

            return darToReturn;
        }

        public async Task<DailyActivityReportDTO> GetLastDailyActivityReportAsync()
        {
            var result = await _darRepository.FindRange(x => x.Id > -1);
            var dar = result.LastOrDefault();
            var id = dar.Id;

            if (dar == null)
                throw new ArgumentNullException(null, "Could not find a Daily Activity Report with the given date.");

            var entries =  await _entryRepository
                .FindRange(x => x.ParentEntry == null && x.DailyActivityReportId == dar.Id);
            
            var darToReturn = _mapper.Map<DailyActivityReportDTO>(dar);
            var darEntries = _mapper.Map<ICollection<EntryDTO>>(entries);

            darToReturn.Entries = darEntries;

            return darToReturn;
        }

        public async Task DeleteEntryFromDailyActivityReportAsync(int entryId, int darId) 
        {
            var entry = await FindEntry(entryId);
            var dar = await FindDailyActivityReport(darId);
            
            if (entry.DailyActivityReportId != dar.Id)
                throw new ApplicationException("This entry does not belong to the Daily Activity Report.");

            dar.Entries.Remove(entry);
            _darRepository.Update(dar);

            if (! await _darRepository.SaveAll())
                throw new ApplicationException("Something went wrong removing the entry from the Daily Activity Report.");
        }

        public async Task DeleteDailyActivityReportAsync(int darId) 
        {
            var dar = await FindDailyActivityReport(darId);

            _darRepository.Remove(dar);

            if (! await _darRepository.SaveAll())
                throw new ApplicationException("Something went wrong deleting the Daily Activity Report.");
        }
        
        public async Task UpdateEntryInDailyActivityReportAsync(AddEntryToDailyActivityReportDTO dto) 
        {
            var dar = await FindDailyActivityReport(dto.DailyActivityReport.Id);
            var entry = await FindEntry(dto.Entry.Id);
            var updatedEntry = _mapper.Map<Entry>(dto.Entry);

            if (entry.DailyActivityReport.Id != dar.Id)
                throw new ApplicationException("This entry does not belong to the given Daily Activity Report.");
            
            _mapper.Map(updatedEntry, entry);

            if (! await _entryRepository.SaveAll())
                throw new ApplicationException("Something went wrong, could not update the given entry.");
        }

        private async Task<Entry> FindEntry(int entryId) => 
            await _entryRepository.Find(entryId) 
            ?? throw new ArgumentNullException(null, "Could not find an entry with the given id.");

        private async Task<DailyActivityReport> FindDailyActivityReport(int darId) => 
            await _darRepository.Find(darId) 
            ?? throw new ArgumentNullException(null, "Could not find a Daily Activity Report with the given id.");
    }
}