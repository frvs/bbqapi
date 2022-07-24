using BarbequeApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BarbequeApi.Controllers
{
    [ApiController]
    [Route("api/barbeques/{barbequeId}/persons")]
    public class PersonController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonDto personDto)
        {

            return NoContent();
        }

        [HttpDelete("{personId}")]
        public async Task<IActionResult> Delete([FromQuery] long personId)
        {
            var personDto = new PersonDto();

            return Ok(personDto);
        }
    }
}
