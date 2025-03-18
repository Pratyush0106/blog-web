using blogapi.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using blogapi.Service;

namespace blogapi.Controllers
{
    [Route("api/contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactFormService _contactFormService;

        public ContactController(IContactFormService contactFormService)
        {
            _contactFormService = contactFormService;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitContactForm([FromBody] ContactFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _contactFormService.SubmitContactFormAsync(model);

            if (result)
            {
                return Ok(new { message = "Contact form submitted successfully" });
            }

            return StatusCode(500, new { message = "Failed to submit contact form" });
        }
    }
}