using Microsoft.AspNetCore.Mvc;

namespace MinimalApiExample.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TableGebruikersController : ControllerBase
    {
        private readonly GebruikerService _service;

        public TableGebruikersController(GebruikerService service)
        {
            _service = service;
        }
        
        // GET: api/Soorten
        [HttpGet]
        public IActionResult HaalAlleTableGebruikersOp()
        {
            var tableGebruikers = _service.HaalAlleTableGebruikersOp();
            return Ok(tableGebruikers);
        }

        // POST: api/Soorten
        [HttpPost]
        public IActionResult VoegTableGebruikerToe([FromBody] TableGebruiker tableGebruiker)
        {
            if (tableGebruiker == null)
            {
                return BadRequest("Gebruiker mag niet null zijn.");
            }

            _service.RegistreerTableGebruiker(tableGebruiker);
            return Ok("Gebruiker toegevoegd.");
        }

        // DELETE: api/Soorten/{naam}
        [HttpDelete("{naam}")]
        public IActionResult VerwijderTableGebruiker(String naam)
        {
            var isVerwijderd = _service.VerwijderTableGebruiker(naam);
            if (!isVerwijderd)
            {
                return NotFound($"Gebruiker met naam {naam} niet gevonden.");
            }

            return Ok($"Gebruiker met naam {naam} is verwijderd.");
        }
    }
}
