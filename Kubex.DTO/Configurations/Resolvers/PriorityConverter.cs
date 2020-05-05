
using System.Threading.Tasks;
using AutoMapper;
using Kubex.Models;
using Kubex.DAL.Repositories.Interfaces;
using System;

namespace Kubex.DTO.Configurations.Resolvers
{
    public class PriorityConverter : 
        ITypeConverter<string, Priority>,
        ITypeConverter<Priority, string>,
        IValueResolver<EntryDTO, Entry, Priority>,
        IValueResolver<Entry, EntryDTO, string>
    {
        private readonly IPriorityRepository _repository;

        public PriorityConverter(IPriorityRepository repository)
        {
            _repository = repository;

        }
        public Priority Convert(string source, Priority destination, ResolutionContext context)
        {
            return Task.Run(async () => {
                var priority = await _repository.FindByLevel(source);
                if (priority == null)
                {
                    _repository.Add(new Priority { Level = source });

                    if (!await _repository.SaveAll())
                        throw new ApplicationException("Something went wrong saving a new priority level to the database.");

                }
                return await _repository.FindByLevel(source);
            }).Result;
        }

        public string Convert(Priority source, string destination, ResolutionContext context)
        {
            return source.Level;
        }

        public Priority Resolve(EntryDTO source, Entry destination, Priority destMember, ResolutionContext context)
        {
            return Convert(source.Priority, destination.Priority, context);
        }

        public string Resolve(Entry source, EntryDTO destination, string destMember, ResolutionContext context)
        {
            return Convert(source.Priority, destination.Priority, context);
        }
    }
}