using BarbequeApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BarbequeApi.Controllers
{
    [ApiController]
    [Route("api/barbeques")]
    public class BarbequeController : Controller
    {
        [HttpGet("{barbequeId}")]
        public async Task<IActionResult> Get([FromQuery] long barbequeId)
        {
            var barbequeDto = new BarbequeDto();

            return Ok(barbequeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BarbequeDto barbequeDto)
        {

            return NoContent();
        }
    }
}
