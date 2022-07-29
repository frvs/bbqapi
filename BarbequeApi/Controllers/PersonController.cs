using BarbequeApi.Models.Dtos;
using BarbequeApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BarbequeApi.Controllers
{
  [ApiController]
  [Route("api/barbeques")]
  public class PersonController : Controller
  {
    private readonly IPersonService service;

    public PersonController(IPersonService service)
    {
      this.service = service;
    }

    [HttpPost("{barbequeId}/persons")]
    public async Task<IActionResult> Create([FromRoute] string barbequeId, [FromBody] PersonDto personDto)
    {
      service.Create(long.Parse(barbequeId), personDto);

      return NoContent();
    }

    [HttpDelete("{barbequeId}/persons/{personId}")]
    public async Task<IActionResult> Delete([FromRoute] string barbequeId, [FromRoute] string personId)
    {
      service.Delete(long.Parse(barbequeId), long.Parse(personId));

      return NoContent();
    }
  }
}
