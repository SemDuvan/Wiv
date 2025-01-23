using Microsoft.AspNetCore.Mvc;

namespace MinimalApiExample.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TableSoortenController : ControllerBase
    {
        private readonly SoortService _service;

        public TableSoortenController(SoortService service)
        {
            _service = service;
        }
        
        // GET: api/Soorten
        [HttpGet]
        public IActionResult HaalAlleTableSoortenOp()
        {
            var tableSoorten = _service.HaalAlleTableSoortenOp();
            return Ok(tableSoorten);
        }

        // POST: api/Soorten
        [HttpPost]
        public IActionResult VoegTableSoortToe([FromBody] TableSoort tableSoort)
        {
            if (tableSoort == null)
            {
                return BadRequest("Soort mag niet null zijn.");
            }

            _service.RegistreerTableSoort(tableSoort);
            return Ok("Soort toegevoegd.");
        }

        // DELETE: api/Soorten/{naam}
        [HttpDelete("{naam}")]
        public IActionResult VerwijderTableSoort(String naam)
        {
            var isVerwijderd = _service.VerwijderTableSoort(naam);
            if (!isVerwijderd)
            {
                return NotFound($"Soort met naam {naam} niet gevonden.");
            }

            return Ok($"Soort met naam {naam} is verwijderd.");
        }
    }
}
