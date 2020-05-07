using System;
using System.Threading.Tasks;
using Kubex.BLL.Services.Interfaces;
using Kubex.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kubex.API.Controllers
{
    [ApiController]
    [Authorize]    
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) 
        {
            var post = await _postService.GetPostAsync(id);

            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            await _postService.DeletePostAsync(id);

            return NoContent();
        }

        [HttpPost("create")]        
        public async Task<IActionResult> Create(PostDTO dto)
        {
            var post = await _postService.CreatePostAsync(dto);

            return Ok(post);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUserToPosts(UpdateUserPostsDTO dto) 
        {
            var updatedUser = await _postService.SetUserPostsAsync(dto);

            return Ok(updatedUser);
        }

        [HttpPost("modify")]
        public async Task<IActionResult> ModifyPostRoles(ModifyPostRolesDTO dto) 
        {
            var post = await _postService.SetPostRolesAsync(dto);

            return Ok(post);
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdatePost(int id, UpdatePostDTO dto) 
        {
            var post = await _postService.UpdatePostAsync(dto);

            return Ok(post);
        }
    }
}