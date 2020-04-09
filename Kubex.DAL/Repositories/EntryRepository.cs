using Kubex.DAL.Repositories.Interfaces;
using Kubex.Models;

namespace Kubex.DAL.Repositories
{
    public class EntryRepository 
        : Repository<Entry, int>, IEntryRepository
    {
        public EntryRepository(DataContext context)
            : base(context)
        {

        }
    }
}