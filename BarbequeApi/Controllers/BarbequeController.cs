using BarbequeApi.Models.Dtos;
using BarbequeApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BarbequeApi.Controllers
{
    [ApiController]
    [Route("api/barbeques")]
    public class BarbequeController : Controller
    {
        private readonly IBarbequeService service;

        public BarbequeController(IBarbequeService service)
        {
            this.service = service;
        }

        [HttpGet("{barbequeId}")]
        public async Task<IActionResult> Get([FromRoute] string barbequeId)
        {
            var bbqIdParsed = long.Parse(barbequeId);

            if (bbqIdParsed <= 0) 
            {
                return BadRequest(); // NotFound is a good option too.
            }

            var barbequeDto = service.Get(bbqIdParsed);

            if (barbequeDto == null)
            {
                return NotFound();
            }

            return Ok(barbequeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BarbequeDto barbequeDto)
        {
            var (successful, errorMessages) = service.Create(barbequeDto);

            if(!successful)
            {
                return BadRequest(errorMessages);
            }

            return NoContent();
        }
    }
}
