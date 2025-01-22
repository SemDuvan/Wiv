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
        public IActionResult VerwijderTableSoort(String soort)
        {
            var isVerwijderd = _service.VerwijderTableSoort(soort);
            if (!isVerwijderd)
            {
                return NotFound($"Soort met naam {soort} niet gevonden.");
            }

            return Ok($"Soort met naam {soort} is verwijderd.");
        }
    }
}
