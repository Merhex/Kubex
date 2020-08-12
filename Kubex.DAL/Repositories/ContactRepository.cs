using Kubex.DAL.Repositories.Interfaces;
using Kubex.Models;

namespace Kubex.DAL.Repositories
{
    public class ContactRepository 
        : Repository<Contact, int>, IContactRepository
    {
        public ContactRepository(DataContext context)
            : base(context)
        {
            
        }
    }
}