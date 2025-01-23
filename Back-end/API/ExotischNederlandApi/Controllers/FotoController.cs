using Microsoft.AspNetCore.Mvc;

namespace MinimalApiExample.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class FotoController : ControllerBase
    {
        private readonly FotoService _service;

        public FotoController(FotoService service)
        {
            _service = service;
        }
        
        // GET: api/Soorten
        [HttpGet]
        public IActionResult HaalAlleTableFotosOp()
        {
            var tableFotos = _service.HaalAlleTableFotosOp();
            return Ok(tableFotos);
        }

        // POST: api/Soorten
        [HttpPost]
        public IActionResult VoegTableFotoToe([FromBody] TableFoto tableFoto)
        {
            if (tableFoto == null)
            {
                return BadRequest("Foto mag niet null zijn.");
            }

            _service.RegistreerTableFoto(tableFoto);
            return Ok("Foto toegevoegd.");
        }

        // DELETE: api/Soorten/{naam}
        [HttpDelete("{naam}")]
        public IActionResult VerwijderTableFoto(String naam)
        {
            var isVerwijderd = _service.VerwijderTableFoto(naam);
            if (!isVerwijderd)
            {
                return NotFound($"Foto met naam {naam} niet gevonden.");
            }

            return Ok($"Foto met naam {naam} is verwijderd.");
        }
    }
}
