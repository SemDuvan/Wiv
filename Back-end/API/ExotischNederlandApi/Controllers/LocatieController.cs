using Microsoft.AspNetCore.Mvc;

namespace MinimalApiExample.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TableLocatiesController : ControllerBase
    {
        private readonly LocatieService _service;

        public TableLocatiesController(LocatieService service)
        {
            _service = service;
        }
        
        // GET: api/Soorten
        [HttpGet]
        public IActionResult HaalAlleTableLocatiesOp()
        {
            var tableLocaties = _service.HaalAlleTableLocatiesOp();
            return Ok(tableLocaties);
        }

        // POST: api/Soorten
        [HttpPost]
        public IActionResult VoegTableLocatieToe([FromBody] TableLocatie tableLocatie)
        {
            if (tableLocatie == null)
            {
                return BadRequest("Locatie mag niet null zijn.");
            }

            _service.RegistreerTableLocatie(tableLocatie);
            return Ok("Locatie toegevoegd.");
        }

        // DELETE: api/Soorten/{naam}
        [HttpDelete("{naam}")]
        public IActionResult VerwijderTableLocatie(String naam)
        {
            var isVerwijderd = _service.VerwijderTableLocatie(naam);
            if (!isVerwijderd)
            {
                return NotFound($"Locatie met naam {naam} niet gevonden.");
            }

            return Ok($"Locatie met naam {naam} is verwijderd.");
        }
    }
}
