using Konyvtar.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace KonyvtarSzerver.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KonyvtarController : ControllerBase
    {
        private readonly KonyvtarSzerverContext _konyvtarSzerverContext;
        private readonly ILogger<KonyvtarController> _logger;

        private bool ContainsSpecialCharacters(string input)
        {
            return Regex.IsMatch(input, @"[^a-zA-Z0-9\s]+");
        }

        private bool CheckWhitespaceViolation(string input)
        {
            bool isFirstCharacterWhitespace = Regex.IsMatch(input, @"^\s");
            bool isLastCharacterWhitespace = Regex.IsMatch(input, @"\s$");
            bool hasConsecutiveWhitespaces = Regex.IsMatch(input, @"\s\s");

            return isFirstCharacterWhitespace || isLastCharacterWhitespace || hasConsecutiveWhitespaces;
        }

        public KonyvtarController(KonyvtarSzerverContext konyvtarSzerverContext, ILogger<KonyvtarController> logger)
        {
            _konyvtarSzerverContext = konyvtarSzerverContext;
            _logger = logger;
        }

        [HttpGet("konyvek")]
        public async Task<ActionResult<IEnumerable<Konyv>>> GetKonyvek()
        {
            _logger.LogInformation("Konyvek endpoint was called");
            var konyvek = await _konyvtarSzerverContext.Konyv.ToListAsync();
            return Ok(konyvek);
        }

        [HttpGet("tagok")]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTagok()
        {
            _logger.LogInformation("Tagok endpoint was called");
            var tagok = await _konyvtarSzerverContext.Tag.ToListAsync();
            return Ok(tagok);
        }

        [HttpGet("kolcsonzesek")]
        public async Task<ActionResult<IEnumerable<Kolcsonzes>>> GetKolcsonzesek()
        {
            _logger.LogInformation("Kolcsonzesek endpoint was called");
            var kolcsonzesek = await _konyvtarSzerverContext.Kolcsonzes.ToListAsync();
            return Ok(kolcsonzesek);
        }

        [HttpGet("konyv/{id}")]
        public async Task<ActionResult<Konyv>> GetKonyv(int id)
        {
            var konyv = await _konyvtarSzerverContext.Konyv.FindAsync(id);

            if (konyv is null)
            {
                return NotFound();
            }

            return Ok(konyv);
        }

        [HttpGet("tag/{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            var tag = await _konyvtarSzerverContext.Tag.FindAsync(id);

            if (tag is null)
            {
                return NotFound();
            }

            return Ok(tag);
        }

        [HttpGet("kolcsonzes/{id}")]
        public async Task<ActionResult<Tag>> GetKolcsonzes(int id)
        {
            var kolcsonzes = await _konyvtarSzerverContext.Kolcsonzes.FindAsync(id);

            if (kolcsonzes is null)
            {
                return NotFound();
            }

            return Ok(kolcsonzes);
        }

        [HttpPost("konyv")]
        public async Task<IActionResult> PostKonyv([FromBody] Konyv konyv)
        {
            if (ContainsSpecialCharacters(konyv.Cim))
            {
                return BadRequest();
            }

            _konyvtarSzerverContext.Konyv.Add(konyv);
            await _konyvtarSzerverContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("tag")]
        public async Task<IActionResult> PostTag([FromBody] Tag tag)
        {
            if (tag.Nev.Length == 0 || ContainsSpecialCharacters(tag.Nev) || CheckWhitespaceViolation(tag.Nev))
            {
                return BadRequest();
            }

            _konyvtarSzerverContext.Tag.Add(tag);
            await _konyvtarSzerverContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("kolcsonzes")]
        public async Task<IActionResult> PostKolcsonzes([FromBody] Kolcsonzes kolcsonzes)
        {
            if (kolcsonzes.KolcsonzesIdeje >= kolcsonzes.VisszahozasIdeje)
            {
                return BadRequest();
            }

            _konyvtarSzerverContext.Kolcsonzes.Add(kolcsonzes);
            await _konyvtarSzerverContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("konyv/{id}")]
        public async Task<IActionResult> DeleteKonyv(int id)
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

        [HttpDelete("tag/{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var existingTag = await _konyvtarSzerverContext.Tag.FindAsync(id);

            if (existingTag is null)
            {
                return NotFound();
            }

            _konyvtarSzerverContext.Tag.Remove(existingTag);
            await _konyvtarSzerverContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("kolcsonzes/{id}")]
        public async Task<IActionResult> DeleteKolcsonzes(int id)
        {
            var existingKolcsonzes = await _konyvtarSzerverContext.Kolcsonzes.FindAsync(id);

            if (existingKolcsonzes is null)
            {
                return NotFound();
            }

            _konyvtarSzerverContext.Kolcsonzes.Remove(existingKolcsonzes);
            await _konyvtarSzerverContext.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("konyv/{id}")]
        public async Task<IActionResult> PutKonyv(int id, [FromBody] Konyv konyv)
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

        [HttpPut("tag/{id}")]
        public async Task<IActionResult> PutTag(int id, [FromBody] Tag tag)
        {
            if (id != tag.Id)
            {
                return BadRequest();
            }

            var existingTag = await _konyvtarSzerverContext.Tag.FindAsync(id);

            if (existingTag is null)
            {
                return NotFound();
            }

            existingTag.Nev = tag.Nev;
            existingTag.Lakcim = tag.Lakcim;
            existingTag.OlvasoSzam = tag.OlvasoSzam;
            existingTag.SzuletesiDatum = tag.SzuletesiDatum;
            await _konyvtarSzerverContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("kolcsonzes/{id}")]
        public async Task<IActionResult> PutKolcsonzes(int id, [FromBody] Kolcsonzes kolcsonzes)
        {
            if (id != kolcsonzes.Id)
            {
                return BadRequest();
            }

            var existingKolcsonzes = await _konyvtarSzerverContext.Kolcsonzes.FindAsync(id);

            if (existingKolcsonzes is null)
            {
                return NotFound();
            }

            if (existingKolcsonzes.KolcsonzesIdeje >= existingKolcsonzes.VisszahozasIdeje)
            {
                return BadRequest();
            }    

            existingKolcsonzes.OlvasoSzam = kolcsonzes.OlvasoSzam;
            existingKolcsonzes.LeltariSzam = kolcsonzes.LeltariSzam;
            existingKolcsonzes.KolcsonzesIdeje = kolcsonzes.KolcsonzesIdeje;
            existingKolcsonzes.VisszahozasIdeje = kolcsonzes.VisszahozasIdeje;
            await _konyvtarSzerverContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
