using System.Threading.Tasks;
using Kubex.BLL.Services.Interfaces;
using Kubex.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kubex.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;

        }

        [HttpPost("/add")]
        public async Task<IActionResult> AddContact(ContactDTO dto) 
        {
            var contact = await _contactService.CreateContactAsync(dto);

            return Ok(contact);
        }
    }
}