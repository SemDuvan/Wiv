using Microsoft.AspNetCore.Mvc;

namespace MinimalApiExample.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TableSoortenController : ControllerBase
    {
        private readonly TableSoortService _service;

        public TableSoortenController(TableSoortService service)
        {
            _service = service;
        }
        
        // GET: api/Soorten
        [HttpGet]
        public IActionResult HaalAlleSoortenOp()
        {
            var soorten = _service.HaalAlleSoortenOp();
            return Ok(soorten);
        }

        // POST: api/Soorten
        [HttpPost]
        public IActionResult VoegSoortToe([FromBody] TableSoort Soort)
        {
            if (Soort == null)
            {
                return BadRequest("Soort mag niet null zijn.");
            }

            _service.RegistreerInheemseSoort(Soort);
            return Ok("Soort toegevoegd.");
        }

        // DELETE: api/Soorten/{naam}
        [HttpDelete("{naam}")]
        public IActionResult VerwijderSoort(String naam)
        {
            var isVerwijderd = _service.VerwijderSoort(naam);
            if (!isVerwijderd)
            {
                return NotFound($"Soort met naam {naam} niet gevonden.");
            }

            return Ok($"Soort met naam {naam} is verwijderd.");
        }
    }
}
