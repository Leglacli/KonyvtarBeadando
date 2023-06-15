// <copyright file="UgyfelService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UgyfelKliens.Blazor.Services
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Json;
    using Konyvtar.Contracts;

    public class UgyfelService : IUgyfelService
    {
        private readonly HttpClient httpClient;

        public UgyfelService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Retrieves all Konyv objects asynchronously.
        /// </summary>
        /// <returns>A task result which contains the collection of retrieved Konyv objects if found, otherwise it returns null.</returns>
        public Task<IEnumerable<Konyv>?> GetAllKonyvAsync() =>
            this.httpClient.GetFromJsonAsync<IEnumerable<Konyv>>("konyvtar/konyvek");

        /// <summary>
        /// Retrieves all Tag objects asynchronously.
        /// </summary>
        /// <returns>A task result which contains the collection of retrieved Tag objects if found, otherwise it returns null.</returns>
        public Task<IEnumerable<Tag>?> GetAllTagAsync() =>
            this.httpClient.GetFromJsonAsync<IEnumerable<Tag>>("konyvtar/tagok");

        /// <summary>
        /// Retrieves all Kolcsonzes objects asynchronously.
        /// </summary>
        /// <returns>A task result which contains the collection of retrieved Kolcsonzes objects if found, otherwise it returns null.</returns>
        public Task<IEnumerable<Kolcsonzes>?> GetAllKolcsonzesAsync() =>
            this.httpClient.GetFromJsonAsync<IEnumerable<Kolcsonzes>>("konyvtar/kolcsonzesek");

        /// <summary>
        /// Retrieves a Konyv object asynchronously based on the specified leltari szam(ID).
        /// </summary>
        /// <param name="leltariSzam">The ID of the Konyv object to retrieve.</param>
        /// <returns>A task result which contains the retrieved Konyv object if found, otherwise it returns null.</returns>
        public Task<Konyv?> GetKonyvAsync(int leltariSzam) =>
            this.httpClient.GetFromJsonAsync<Konyv?>($"konyvtar/konyv/{leltariSzam}");

        /// <summary>
        /// Retrieves a Tag object asynchronously based on the specified olvaso szam(ID).
        /// </summary>
        /// <param name="olvasoSzam">The ID of the Tag object to retrieve.</param>
        /// <returns>A task result which contains the retrieved Tag object if found, otherwise it returns null.</returns>
        public Task<Tag?> GetTagAsync(int olvasoSzam) =>
            this.httpClient.GetFromJsonAsync<Tag?>($"konyvtar/tag/{olvasoSzam}");

        /// <summary>
        /// Retrieves a Kolcsonzes object asynchronously based on the specified ID.
        /// </summary>
        /// <param name="id">The ID of the Kolcsonzes object to retrieve.</param>
        /// <returns>A task result which contains the retrieved Kolcsonzes object if found, otherwise it returns null.</returns>
        public Task<Kolcsonzes?> GetKolcsonzesAsync(int id) =>
            this.httpClient.GetFromJsonAsync<Kolcsonzes?>($"konyvtar/kolcsonzes/{id}");

        /// <summary>
        /// Adds a new Konyv asynchronously.
        /// </summary>
        /// <param name="konyv">The Konyv object to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddKonyvAsync(Konyv konyv) =>
            await this.httpClient.PostAsJsonAsync("konyvtar/konyv", konyv);

        /// <summary>
        /// Adds a new Tag asynchronously.
        /// </summary>
        /// <param name="tag">The Tag object to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddTagAsync(Tag tag) =>
            await this.httpClient.PostAsJsonAsync("konyvtar/tag", tag);

        /// <summary>
        /// Adds a new Kolcsonzes asynchronously.
        /// </summary>
        /// <param name="kolcsonzes">The Kolcsonzes object to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddKolcsonzesAsync(Kolcsonzes kolcsonzes) =>
            await this.httpClient.PostAsJsonAsync("konyvtar/kolcsonzes", kolcsonzes);

        /// <summary>
        /// Deletes a Konyv asynchronously based on the specified leltari szam(ID).
        /// </summary>
        /// <param name="leltariSzam">The leltari szam(ID) of the Konyv to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteKonyvAsync(int leltariSzam) =>
           await this.httpClient.DeleteAsync($"konyvtar/konyv/{leltariSzam}");

        /// <summary>
        /// Deletes a Tag asynchronously based on the specified olvaso szam(ID).
        /// </summary>
        /// <param name="olvasoSzam">The olvaso szam(ID) of the Tag to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteTagAsync(int olvasoSzam) =>
            await this.httpClient.DeleteAsync($"konyvtar/tag/{olvasoSzam}");

        /// <summary>
        /// Deletes a Kolcsonzes asynchronously based on the specified ID.
        /// </summary>
        /// <param name="id">The ID of the Kolcsonzes to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteKolcsonzesAsync(int id) =>
            await this.httpClient.DeleteAsync($"konyvtar/kolcsonzes/{id}");
    }
}
