using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.Models;

namespace Kubex.DTO.Configurations.Resolvers
{
    public class PostResolver : IValueResolver<User, UserToReturnDTO, IEnumerable<int>>
    {
        private readonly IPostRepository _postRepository;

        public PostResolver(IPostRepository postRepository)
        {
            _postRepository = postRepository;

        }

        public IEnumerable<int> Resolve(User source, UserToReturnDTO destination, IEnumerable<int> destMember, ResolutionContext context)
        {
            return Task.Run(async () => 
            {
                var posts = await _postRepository.FindRange(p => p.Users.Where(u => u.UserId == source.Id).Any());
                return posts.Select(x => x.Id);
            }).Result;
        }
    }
}