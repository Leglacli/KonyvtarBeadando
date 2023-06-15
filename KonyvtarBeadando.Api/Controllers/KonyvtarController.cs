// <copyright file="KonyvtarController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KonyvtarSzerver.Api.Controllers
{
    using System.Text.RegularExpressions;
    using Konyvtar.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Represents a controller for managing the Kony, Tag and Kolcsonzes entities.
    /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="KonyvtarController"/> class.
        /// </summary>
        /// <param name="konyvtarSzerverContext">The KonyvtarSzerverContext instance used for database access.</param>
        /// <param name="logger">The ILogger instance used for logging.</param>
        public KonyvtarController(KonyvtarSzerverContext konyvtarSzerverContext, ILogger<KonyvtarController> logger)
        {
            this.konyvtarSzerverContext = konyvtarSzerverContext;
            this.logger = logger;
        }

        /// <summary>
        /// Retrieves a list of Konyv objects.
        /// </summary>
        /// <returns>An ActionResult containing a list of Konyv objects. If the operation fails, an error response is returned.</returns>
        [HttpGet("konyvek")]
        public async Task<ActionResult<IEnumerable<Konyv>>> GetKonyvek()
        {
            this.logger.LogInformation("Konyvek endpoint was called");
            var konyvek = await this.konyvtarSzerverContext.Konyv.ToListAsync();
            return this.Ok(konyvek);
        }

        /// <summary>
        /// Retrieves a list of Tag objects.
        /// </summary>
        /// <returns>An ActionResult containing a list of Tag objects. If the operation fails, an error response is returned.</returns>
        [HttpGet("tagok")]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTagok()
        {
            this.logger.LogInformation("Tagok endpoint was called");
            var tagok = await this.konyvtarSzerverContext.Tag.ToListAsync();
            return this.Ok(tagok);
        }

        /// <summary>
        /// Retrieves a list of Kolcsonzes objects.
        /// </summary>
        /// <returns>An ActionResult containing a list of Kolcsonzes objects. If the operation fails, an error response is returned.</returns>
        [HttpGet("kolcsonzesek")]
        public async Task<ActionResult<IEnumerable<Kolcsonzes>>> GetKolcsonzesek()
        {
            this.logger.LogInformation("Kolcsonzesek endpoint was called");
            var kolcsonzesek = await this.konyvtarSzerverContext.Kolcsonzes.ToListAsync();
            return this.Ok(kolcsonzesek);
        }

        /// <summary>
        /// Retrieves a Konyv object by the specified leltari szam(ID).
        /// </summary>
        /// <param name="leltariSzam">The inventory number of the Konyv.</param>
        /// <returns>An ActionResult containing the found Konyv if it exists, otherwise, a NotFound response.</returns>
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

        /// <summary>
        /// Retrieves a Tag object by the specified olvaso szam(ID).
        /// </summary>
        /// <param name="olvasoSzam">The reader number of the Tag.</param>
        /// <returns>An ActionResult containing the found Tag if it exists, otherwise, a NotFound response.</returns>
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

        /// <summary>
        /// Retrieves a Kolcsonzes object by the specified ID.
        /// </summary>
        /// <param name="id">The ID of the Kolcsonzes.</param>
        /// <returns>An ActionResult containing the found Kolcsonzes if it exists, otherwise, a NotFound response.</returns>
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

        /// <summary>
        /// Creates a new Konyv object.
        /// </summary>
        /// <param name="konyv">The Konyv object to be created.</param>
        /// <returns>An Ok IActionResult if the Konyv object is successfully created, otherwise, returns a BadRequest.</returns>
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

        /// <summary>
        /// Creates a new Tag object.
        /// </summary>
        /// <param name="tag">The Tag object to be created.</param>
        /// <returns>An Ok IActionResult if the Tag object is successfully created, otherwise, returns a BadRequest.</returns>
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

        /// <summary>
        /// Creates a new Kolcsonzes object.
        /// </summary>
        /// <param name="kolcsonzes">The Kolcsonzes object to be created.</param>
        /// <returns>An Ok IActionResult if the Kolcsonzes object is successfully created, otherwise, returns a BadRequest.</returns>
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

        /// <summary>
        /// Deletes a Konyv object by its leltari szam.
        /// </summary>
        /// <param name="leltariSzam">The leltariSzam of the Konyv object to be deleted.</param>
        /// <returns>A NoContent IActionResult if the Konyv object is successfully deleted, otherwise, returns NotFound.</returns>
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

        /// <summary>
        /// Deletes a Tag object by its olvaso szam.
        /// </summary>
        /// <param name="olvasoSzam">The olvasoSzam of the Tag object to be deleted.</param>
        /// <returns>A NoContent IActionResult if the Tag object is successfully deleted, otherwise, returns NotFound.</returns>
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

        /// <summary>
        /// Deletes a Kolcsonzes object by its id.
        /// </summary>
        /// <param name="id">The id of the Kolcsonzes object to be deleted.</param>
        /// <returns>A NoContent IActionResult if the Kolcsonzes object is successfully deleted, otherwise, returns NotFound.</returns>
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

        /// <summary>
        /// Updates an existing Konyv object with the specified leltari szam.
        /// </summary>
        /// <param name="leltariSzam">The leltariSzam of the Konyv object to be updated.</param>
        /// <param name="konyv">The updated Konyv object.</param>
        /// <returns>A NoContent IActionResult if the Konyv object is successfully updated, otherwise,
        /// returns BadRequest if the leltariSzam of the Konyv object in the request body does not match the specified leltari szam or
        /// NotFound if the Konyv object with the specified leltari szam is not found.</returns>
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

        /// <summary>
        /// Updates an existing Tag object with the specified olvaso szam.
        /// </summary>
        /// <param name="olvasoSzam">The olvasoSzam of the Tag object to be updated.</param>
        /// <param name="tag">The updated Tag object.</param>
        /// <returns>A NoContent IActionResult if the Tag object is successfully updated, otherwise,
        /// returns BadRequest if the olvasoSzam of the Tag object in the request body does not match the specified olvaso szam or
        /// NotFound if the Tag object with the specified olvaso szam is not found.</returns>
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

        /// <summary>
        /// Updates an existing Kolcsonzes object with the specified id.
        /// </summary>
        /// <param name="id">The id of the Kolcsonzes object to be updated.</param>
        /// <param name="kolcsonzes">The updated Kolcsonzes object.</param>
        /// <returns>A NoContent IActionResult if the Kolcsonzes object is successfully updated, otherwise,
        /// returns BadRequest if the id of the Kolcsonzes object in the request body does not match the specified id or
        /// NotFound if the Kolcsonzes object with the specified id is not found.</returns>
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
