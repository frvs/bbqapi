using BarbequeApi.Models.Dtos;
using BarbequeApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
      var (successful, errorMessages) = service.Create(barbequeId, personDto);

      if(!successful)
      {
        switch (errorMessages.First()[..3])
        {
          case "400": return BadRequest(errorMessages);
          case "404": return NotFound(errorMessages);
        }
      }

      return NoContent();
    }

    [HttpDelete("{barbequeId}/persons/{personId}")]
    public async Task<IActionResult> Delete([FromRoute] string barbequeId, [FromRoute] string personId)
    {
      var (successful, errorMessages) = service.Delete(barbequeId, personId);

      if(!successful)
      {
        switch(errorMessages.First()[..3])
        {
          case "400": return BadRequest(errorMessages);
          case "404": return NotFound(errorMessages);
        }
      }

      return NoContent();
    }
  }
}
