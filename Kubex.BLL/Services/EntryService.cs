using System;
using System.Threading.Tasks;
using AutoMapper;
using Kubex.BLL.Services.Interfaces;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.DTO;
using Kubex.Models;

namespace Kubex.BLL.Services
{
    public class EntryService : IEntryService
    {
        private readonly IEntryRepository _repository;
        private readonly IMapper _mapper;

        public EntryService(IEntryRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EntryDTO> CreateEntryAsync(EntryDTO dto)
        {
            var entry = _mapper.Map<Entry>(dto);

            _repository.Add(entry);

            if (await _repository.SaveAll())
            {
                return dto;
            }

            throw new ApplicationException("Something went wrong creating the entry.");
        }
    }
}