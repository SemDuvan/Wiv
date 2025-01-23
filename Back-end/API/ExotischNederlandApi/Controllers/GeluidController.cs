using Microsoft.AspNetCore.Mvc;

namespace MinimalApiExample.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class GeluidsController : ControllerBase
    {
        private readonly GeluidService _service;

        public GeluidsController(GeluidService service)
        {
            _service = service;
        }
        
        // GET: api/Soorten
        [HttpGet]
        public IActionResult HaalAlleGeluidsOp()
        {
            var Geluids = _service.HaalAlleGeluidsOp();
            return Ok(Geluids);
        }

        // POST: api/Soorten
        [HttpPost]
        public IActionResult VoegGeluidToe([FromBody] Geluid Geluid)
        {
            if (Geluid == null)
            {
                return BadRequest("Geluid mag niet null zijn.");
            }

            _service.RegistreerGeluid(Geluid);
            return Ok("Geluid toegevoegd.");
        }

        // DELETE: api/Soorten/{naam}
        [HttpDelete("{naam}")]
        public IActionResult VerwijderGeluid(String naam)
        {
            var isVerwijderd = _service.VerwijderGeluid(naam);
            if (!isVerwijderd)
            {
                return NotFound($"Geluid met naam {naam} niet gevonden.");
            }

            return Ok($"Geluid met naam {naam} is verwijderd.");
        }
    }
}
