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
        
        [HttpGet("/company/{companyId}")]
        public async Task<IActionResult> GetContactsForCompany(int companyId) 
        {
            var contacts = await _contactService.GetContactsForCompany(companyId);

            return Ok(contacts);
        }

        [HttpGet("/user/{userId}")]
        public async Task<IActionResult> GetContactsForUser(int userId) 
        {
            var contacts = await _contactService.GetContactsForUser(userId);

            return Ok(contacts);
        }
        
        [HttpPost("/add")]
        public async Task<IActionResult> AddContact(ContactDTO dto) 
        {
            var contact = await _contactService.CreateContactAsync(dto);

            return Ok(contact);
        }
    }
}