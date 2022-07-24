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
            var barbequeDto = service.Get(long.Parse(barbequeId));

            return Ok(barbequeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BarbequeDto barbequeDto)
        {
            service.Create(barbequeDto);

            return NoContent();
        }
    }
}
