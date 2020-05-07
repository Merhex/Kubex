using System.Threading.Tasks;
using AutoMapper;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.Models;

namespace Kubex.DTO.Configurations.Resolvers
{
    public class MediaTypeConverter : ITypeConverter<string, MediaType>, ITypeConverter<MediaType, string>
    {
        private readonly IMediaTypeRepository _repository;

        public MediaTypeConverter(IMediaTypeRepository mediaTypeRepository)
        {
            _repository = mediaTypeRepository;
        }

        public MediaType Convert(string source, MediaType destination, ResolutionContext context)
        {
            return Task.Run(async () => {
                return await _repository.FindByType(source);
            }).Result;
        }

        public string Convert(MediaType source, string destination, ResolutionContext context)
        {
            return source.Type;
        }
    }
}