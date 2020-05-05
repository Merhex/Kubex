using System.Threading.Tasks;
using AutoMapper;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.Models;

namespace Kubex.DTO.Configurations.Resolvers
{
    public class EntryTypeConverter : ITypeConverter<string, EntryType>, ITypeConverter<EntryType, string>
    {
        private readonly IEntryTypeRepository _repository;

        public EntryTypeConverter(IEntryTypeRepository repository)
        {
            _repository = repository;

        }
        public EntryType Convert(string source, EntryType destination, ResolutionContext context)
        {
            return Task.Run(async () => {
                return await _repository.FindByType(source);
            }).Result;
        }

        public string Convert(EntryType source, string destination, ResolutionContext context)
        {
            return source.Type;
        }
    }
}