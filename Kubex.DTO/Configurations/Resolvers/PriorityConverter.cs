
using System.Threading.Tasks;
using AutoMapper;
using Kubex.Models;
using Kubex.DAL.Repositories.Interfaces;

namespace Kubex.DTO.Configurations.Resolvers
{
    public class PriorityConverter : ITypeConverter<string, Priority>, ITypeConverter<Priority, string>
    {
        private readonly IPriorityRepository _repository;

        public PriorityConverter(IPriorityRepository repository)
        {
            _repository = repository;

        }
        public Priority Convert(string source, Priority destination, ResolutionContext context)
        {
            return Task.Run(async () => {
                return await _repository.FindByLevel(source);
            }).Result;
        }

        public string Convert(Priority source, string destination, ResolutionContext context)
        {
            return source.Level;
        }
    }
}