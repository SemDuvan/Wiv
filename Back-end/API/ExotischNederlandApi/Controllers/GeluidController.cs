using Microsoft.AspNetCore.Mvc;

namespace MinimalApiExample.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TableGeluidsController : ControllerBase
    {
        private readonly GeluidService _service;

        public TableGeluidsController(GeluidService service)
        {
            _service = service;
        }
        
        // GET: api/Soorten
        [HttpGet]
        public IActionResult HaalAlleTableGeluidsOp()
        {
            var tableGeluids = _service.HaalAlleTableGeluidsOp();
            return Ok(tableGeluids);
        }

        // POST: api/Soorten
        [HttpPost]
        public IActionResult VoegTableGeluidToe([FromBody] TableGeluid tableGeluid)
        {
            if (tableGeluid == null)
            {
                return BadRequest("Geluid mag niet null zijn.");
            }

            _service.RegistreerTableGeluid(tableGeluid);
            return Ok("Geluid toegevoegd.");
        }

        // DELETE: api/Soorten/{naam}
        [HttpDelete("{naam}")]
        public IActionResult VerwijderTableGeluid(String naam)
        {
            var isVerwijderd = _service.VerwijderTableGeluid(naam);
            if (!isVerwijderd)
            {
                return NotFound($"Geluid met naam {naam} niet gevonden.");
            }

            return Ok($"Geluid met naam {naam} is verwijderd.");
        }
    }
}
