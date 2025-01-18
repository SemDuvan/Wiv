using Microsoft.AspNetCore.Mvc;

namespace MinimalApiExample.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class InheemseSoortenController : ControllerBase
    {
        private readonly InheemseSoortService _service;

        public InheemseSoortenController(InheemseSoortService service)
        {
            _service = service;
        }
        
        // GET: api/InheemseSoorten
        [HttpGet]
        public IActionResult HaalAlleInheemseSoortenOp()
        {
            var soorten = _service.HaalAlleInheemseSoortenOp();
            return Ok(soorten);
        }

        // POST: api/InheemseSoorten
        [HttpPost]
        public IActionResult VoegInheemseSoortToe([FromBody] InheemseSoort inheemseSoort)
        {
            if (inheemseSoort == null)
            {
                return BadRequest("Inheemse soort mag niet null zijn.");
            }

            _service.RegistreerInheemseSoort(inheemseSoort);
            return Ok("Inheemse soort toegevoegd.");
        }

        // DELETE: api/InheemseSoorten/{naam}
        [HttpDelete("{naam}")]
        public IActionResult VerwijderInheemseSoort(String naam)
        {
            var isVerwijderd = _service.VerwijderInheemseSoort(naam);
            if (!isVerwijderd)
            {
                return NotFound($"Inheemse soort met naam {naam} niet gevonden.");
            }

            return Ok($"Inheemse soort met naam {naam} is verwijderd.");
        }
    }
}
