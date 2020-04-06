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
    }
}