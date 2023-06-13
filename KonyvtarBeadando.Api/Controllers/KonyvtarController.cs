// <copyright file="KonyvtarController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KonyvtarSzerver.Api.Controllers
{
    using System.Text.RegularExpressions;
    using Konyvtar.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("[controller]")]
    public class KonyvtarController : ControllerBase
    {
        private readonly KonyvtarSzerverContext konyvtarSzerverContext;
        private readonly ILogger<KonyvtarController> logger;

        private bool ContainsSpecialCharacters(string input)
        {
            return Regex.IsMatch(input, @"[^a-zA-Z0-9\sáéíóöőúüűÁÉÍÓÖŐÚÜŰ]+");
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
            this.konyvtarSzerverContext = konyvtarSzerverContext;
            this.logger = logger;
        }

        [HttpGet("konyvek")]
        public async Task<ActionResult<IEnumerable<Konyv>>> GetKonyvek()
        {
            this.logger.LogInformation("Konyvek endpoint was called");
            var konyvek = await this.konyvtarSzerverContext.Konyv.ToListAsync();
            return this.Ok(konyvek);
        }

        [HttpGet("tagok")]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTagok()
        {
            this.logger.LogInformation("Tagok endpoint was called");
            var tagok = await this.konyvtarSzerverContext.Tag.ToListAsync();
            return this.Ok(tagok);
        }

        [HttpGet("kolcsonzesek")]
        public async Task<ActionResult<IEnumerable<Kolcsonzes>>> GetKolcsonzesek()
        {
            this.logger.LogInformation("Kolcsonzesek endpoint was called");
            var kolcsonzesek = await this.konyvtarSzerverContext.Kolcsonzes.ToListAsync();
            return this.Ok(kolcsonzesek);
        }

        [HttpGet("konyv/{leltariSzam}")]
        public async Task<ActionResult<Konyv>> GetKonyv(int leltariSzam)
        {
            var konyv = await this.konyvtarSzerverContext.Konyv.FindAsync(leltariSzam);

            if (konyv is null)
            {
                return this.NotFound();
            }

            return this.Ok(konyv);
        }

        [HttpGet("tag/{olvasoSzam}")]
        public async Task<ActionResult<Tag>> GetTag(int olvasoSzam)
        {
            var tag = await this.konyvtarSzerverContext.Tag.FindAsync(olvasoSzam);

            if (tag is null)
            {
                return this.NotFound();
            }

            return this.Ok(tag);
        }

        [HttpGet("kolcsonzes/{id}")]
        public async Task<ActionResult<Tag>> GetKolcsonzes(int id)
        {
            var kolcsonzes = await this.konyvtarSzerverContext.Kolcsonzes.FindAsync(id);

            if (kolcsonzes is null)
            {
                return this.NotFound();
            }

            return this.Ok(kolcsonzes);
        }

        [HttpPost("konyv")]
        public async Task<IActionResult> PostKonyv([FromBody] Konyv konyv)
        {
            if (this.ContainsSpecialCharacters(konyv.Cim))
            {
                return this.BadRequest();
            }

            this.konyvtarSzerverContext.Konyv.Add(konyv);
            await this.konyvtarSzerverContext.SaveChangesAsync();

            return this.Ok();
        }

        [HttpPost("tag")]
        public async Task<IActionResult> PostTag([FromBody] Tag tag)
        {
            if (tag.Nev.Length == 0 || this.ContainsSpecialCharacters(tag.Nev) || this.CheckWhitespaceViolation(tag.Nev))
            {
                return this.BadRequest();
            }

            this.konyvtarSzerverContext.Tag.Add(tag);
            await this.konyvtarSzerverContext.SaveChangesAsync();

            return this.Ok();
        }

        [HttpPost("kolcsonzes")]
        public async Task<IActionResult> PostKolcsonzes([FromBody] Kolcsonzes kolcsonzes)
        {
            if (kolcsonzes.KolcsonzesIdeje >= kolcsonzes.VisszahozasIdeje)
            {
                return this.BadRequest();
            }

            this.konyvtarSzerverContext.Kolcsonzes.Add(kolcsonzes);
            await this.konyvtarSzerverContext.SaveChangesAsync();

            return this.Ok();
        }

        [HttpDelete("konyv/{leltariSzam}")]
        public async Task<IActionResult> DeleteKonyv(int leltariSzam)
        {
            var existingKonyv = await this.konyvtarSzerverContext.Konyv.FindAsync(leltariSzam);

            if (existingKonyv is null)
            {
                return this.NotFound();
            }

            this.konyvtarSzerverContext.Konyv.Remove(existingKonyv);
            await this.konyvtarSzerverContext.SaveChangesAsync();

            return this.NoContent();
        }

        [HttpDelete("tag/{olvasoSzam}")]
        public async Task<IActionResult> DeleteTag(int olvasoSzam)
        {
            var existingTag = await this.konyvtarSzerverContext.Tag.FindAsync(olvasoSzam);

            if (existingTag is null)
            {
                return this.NotFound();
            }

            this.konyvtarSzerverContext.Tag.Remove(existingTag);
            await this.konyvtarSzerverContext.SaveChangesAsync();

            return this.NoContent();
        }

        [HttpDelete("kolcsonzes/{id}")]
        public async Task<IActionResult> DeleteKolcsonzes(int id)
        {
            var existingKolcsonzes = await this.konyvtarSzerverContext.Kolcsonzes.FindAsync(id);

            if (existingKolcsonzes is null)
            {
                return this.NotFound();
            }

            this.konyvtarSzerverContext.Kolcsonzes.Remove(existingKolcsonzes);
            await this.konyvtarSzerverContext.SaveChangesAsync();

            return this.NoContent();
        }

        [HttpPut("konyv/{leltariSzam}")]
        public async Task<IActionResult> PutKonyv(int leltariSzam, [FromBody] Konyv konyv)
        {
            if (leltariSzam != konyv.LeltariSzam)
            {
                return this.BadRequest();
            }

            var existingKonyv = await this.konyvtarSzerverContext.Konyv.FindAsync(leltariSzam);

            if (existingKonyv is null)
            {
                return this.NotFound();
            }

            existingKonyv.Cim = konyv.Cim;
            existingKonyv.Szerzo = konyv.Szerzo;
            existingKonyv.Kiado = konyv.Kiado;
            existingKonyv.KiadasEve = konyv.KiadasEve;
            await this.konyvtarSzerverContext.SaveChangesAsync();

            return this.NoContent();
        }

        [HttpPut("tag/{olvasoSzam}")]
        public async Task<IActionResult> PutTag(int olvasoSzam, [FromBody] Tag tag)
        {
            if (olvasoSzam != tag.OlvasoSzam)
            {
                return this.BadRequest();
            }

            var existingTag = await this.konyvtarSzerverContext.Tag.FindAsync(olvasoSzam);

            if (existingTag is null)
            {
                return this.NotFound();
            }

            existingTag.Nev = tag.Nev;
            existingTag.Lakcim = tag.Lakcim;
            existingTag.SzuletesiDatum = tag.SzuletesiDatum;
            await this.konyvtarSzerverContext.SaveChangesAsync();

            return this.NoContent();
        }

        [HttpPut("kolcsonzes/{id}")]
        public async Task<IActionResult> PutKolcsonzes(int id, [FromBody] Kolcsonzes kolcsonzes)
        {
            if (id != kolcsonzes.Id)
            {
                return this.BadRequest();
            }

            var existingKolcsonzes = await this.konyvtarSzerverContext.Kolcsonzes.FindAsync(id);

            if (existingKolcsonzes is null)
            {
                return this.NotFound();
            }

            if (existingKolcsonzes.KolcsonzesIdeje >= existingKolcsonzes.VisszahozasIdeje)
            {
                return this.BadRequest();
            }

            existingKolcsonzes.OlvasoSzam = kolcsonzes.OlvasoSzam;
            existingKolcsonzes.LeltariSzam = kolcsonzes.LeltariSzam;
            existingKolcsonzes.KolcsonzesIdeje = kolcsonzes.KolcsonzesIdeje;
            existingKolcsonzes.VisszahozasIdeje = kolcsonzes.VisszahozasIdeje;
            await this.konyvtarSzerverContext.SaveChangesAsync();

            return this.NoContent();
        }
    }
}
