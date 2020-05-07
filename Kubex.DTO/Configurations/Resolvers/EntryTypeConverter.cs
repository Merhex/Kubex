using System;
using System.Threading.Tasks;
using AutoMapper;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.Models;

namespace Kubex.DTO.Configurations.Resolvers
{
    public class EntryTypeConverter : 
        ITypeConverter<string, EntryType>,
        ITypeConverter<EntryType, string>,
        IValueResolver<EntryDTO, Entry, EntryType>,
        IValueResolver<Entry, EntryDTO, string>
    {
        private readonly IEntryTypeRepository _repository;

        public EntryTypeConverter(IEntryTypeRepository repository)
        {
            _repository = repository;

        }
        public EntryType Convert(string source, EntryType destination, ResolutionContext context)
        {
            return Task.Run(async () => {
                var entryType = await _repository.FindByType(source);
                if (entryType == null)
                {
                    _repository.Add(new EntryType { Type = source });

                    if (!await _repository.SaveAll())
                        throw new ApplicationException("Something went wrong saving a new entry type to the database.");
                }
                return await _repository.FindByType(source);
            }).Result;
        }

        public string Convert(EntryType source, string destination, ResolutionContext context)
        {
            return source.Type;
        }

        public EntryType Resolve(EntryDTO source, Entry destination, EntryType destMember, ResolutionContext context)
        {
            return Convert(source.EntryType, destination.EntryType, context);
        }

        public string Resolve(Entry source, EntryDTO destination, string destMember, ResolutionContext context)
        {
            return Convert(source.EntryType, destination.EntryType, context);
        }
    }
}