using System.Linq;
using System.Threading.Tasks;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.Models;

namespace Kubex.DAL.Repositories
{
    public class CompanyRepository 
        : Repository<Company, int>, ICompanyRepository
    {
        public CompanyRepository(DataContext context)
            : base(context) { }

        public async Task<Company> FindByNameAsync(string name) 
        {
            var companies = await FindRange(x => x.Name == name);
            return companies.FirstOrDefault();
        }
    }
}