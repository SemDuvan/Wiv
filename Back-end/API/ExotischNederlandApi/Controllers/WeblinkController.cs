using Microsoft.AspNetCore.Mvc;

namespace MinimalApiExample.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TableWeblinksController : ControllerBase
    {
        private readonly WeblinkService _service;

        public TableWeblinksController(WeblinkService service)
        {
            _service = service;
        }
        
        // GET: api/Soorten
        [HttpGet]
        public IActionResult HaalAlleTableWeblinksOp()
        {
            var tableWeblinks = _service.HaalAlleTableWeblinksOp();
            return Ok(tableWeblinks);
        }

        // POST: api/Soorten
        [HttpPost]
        public IActionResult VoegTableWeblinkToe([FromBody] TableWeblink tableWeblink)
        {
            if (tableWeblink == null)
            {
                return BadRequest("Weblink mag niet null zijn.");
            }

            _service.RegistreerTableWeblink(tableWeblink);
            return Ok("Weblink toegevoegd.");
        }

        // DELETE: api/Soorten/{naam}
        [HttpDelete("{naam}")]
        public IActionResult VerwijderTableWeblink(String naam)
        {
            var isVerwijderd = _service.VerwijderTableWeblink(naam);
            if (!isVerwijderd)
            {
                return NotFound($"Weblink met naam {naam} niet gevonden.");
            }

            return Ok($"Weblink met naam {naam} is verwijderd.");
        }
    }
}
