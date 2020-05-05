using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.Models;
using Microsoft.EntityFrameworkCore;

namespace Kubex.DAL.Repositories
{
    public class EntryRepository
        : Repository<Entry, int>, IEntryRepository
    {
        public EntryRepository(DataContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Entry>> GetEntriesForDailyActivityReportAsync(int darId) 
        {
            var entries = await FindRange(e => e.DailyActivityReportId == darId);
            return entries;
        }

        public async Task<IEnumerable<Entry>> GetChildEntriesAsync(int entryId) 
        {
            var childEntries = await FindRange(e => e.ParentEntryId == entryId);
            return childEntries;
        }
    }
}