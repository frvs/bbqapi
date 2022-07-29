using BarbequeApi.Models.Dtos;
using BarbequeApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
      var (barbequeDto, errorMessages) = service.Get(barbequeId);


      if (barbequeDto == null || errorMessages.Any())
      {
        switch (errorMessages.First()[..3])
        {
          case "400": return BadRequest(errorMessages);
          case "404": return NotFound(errorMessages);
        }
      }

      return Ok(barbequeDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BarbequeDto barbequeDto)
    {
      var (successful, errorMessages) = service.Create(barbequeDto);

      if (!successful)
      {
        switch (errorMessages.First()[..3])
        {
          case "400": return BadRequest(errorMessages);
          case "404": return NotFound(errorMessages);
        }
      }

      return NoContent();
    }
  }
}
