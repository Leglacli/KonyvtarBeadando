using Konyvtar.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace KonyvtarSzerver.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KonyvtarController : ControllerBase
    {
        private readonly KonyvtarSzerverContext _konyvtarSzerverContext;
        private readonly ILogger<KonyvtarController> _logger;

        public KonyvtarController(KonyvtarSzerverContext konyvtarSzerverContext, ILogger<KonyvtarController> logger)
        {
            _konyvtarSzerverContext = konyvtarSzerverContext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Konyv>>> Get()
        {
            _logger.LogInformation("Konyv endpoint was called");
            var konyv = await _konyvtarSzerverContext.Konyv.ToListAsync();
            return Ok(konyv);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Konyv>> Get(int id)
        {
            var konyv = await _konyvtarSzerverContext.Konyv.FindAsync(id);

            if (konyv is null)
            {
                return NotFound();
            }

            return Ok(konyv);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Konyv konyv)
        {
            _konyvtarSzerverContext.Konyv.Add(konyv);
            await _konyvtarSzerverContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingKonyv = await _konyvtarSzerverContext.Konyv.FindAsync(id);

            if (existingKonyv is null)
            {
                return NotFound();
            }

            _konyvtarSzerverContext.Konyv.Remove(existingKonyv);
            await _konyvtarSzerverContext.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Konyv konyv)
        {
            if (id != konyv.Id)
            {
                return BadRequest();
            }

            var existingKonyv = await _konyvtarSzerverContext.Konyv.FindAsync(id);

            if (existingKonyv is null)
            {
                return NotFound();
            }

            existingKonyv.Cim = konyv.Cim;
            existingKonyv.Szerzo = konyv.Szerzo;
            existingKonyv.Kiado = konyv.Kiado;
            existingKonyv.LeltariSzam = konyv.LeltariSzam;
            existingKonyv.KiadasEve = konyv.KiadasEve;
            await _konyvtarSzerverContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
