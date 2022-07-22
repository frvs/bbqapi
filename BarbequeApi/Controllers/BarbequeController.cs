using BarbequeApi.Models.Dtos;
using BarbequeApi.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Get([FromQuery] long barbequeId)
        {
            var barbequeDto = service.Get(barbequeId);

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
