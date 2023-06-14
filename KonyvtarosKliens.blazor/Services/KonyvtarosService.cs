// <copyright file="KonyvtarosService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KonyvtarosKliens.Blazor.Services
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Json;
    using Konyvtar.Contracts;

    public class KonyvtarosService : IKonyvtarosService
    {
        private readonly HttpClient httpClient;

        public KonyvtarosService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<IEnumerable<Konyv>?> GetAllKonyvAsync() =>
            this.httpClient.GetFromJsonAsync<IEnumerable<Konyv>>("konyvtar/konyvek");

        public Task<IEnumerable<Tag>?> GetAllTagAsync() =>
            this.httpClient.GetFromJsonAsync<IEnumerable<Tag>>("konyvtar/tagok");

        public Task<IEnumerable<Kolcsonzes>?> GetAllKolcsonzesAsync() =>
            this.httpClient.GetFromJsonAsync<IEnumerable<Kolcsonzes>>("konyvtar/kolcsonzesek");

        public Task<Konyv?> GetKonyvAsync(int id) =>
            this.httpClient.GetFromJsonAsync<Konyv?>($"konyvtar/konyv/{id}");

        public Task<Tag?> GetTagAsync(int id) =>
            this.httpClient.GetFromJsonAsync<Tag?>($"konyvtar/tag/{id}");

        public Task<Kolcsonzes?> GetKolcsonzesAsync(int id) =>
            this.httpClient.GetFromJsonAsync<Kolcsonzes?>($"konyvtar/kolcsonzes/{id}");

        public async Task AddTagAsync(Tag tag) =>
            await this.httpClient.PostAsJsonAsync("konyvtar/tag", tag);

        public async Task AddKolcsonzesAsync(Kolcsonzes kolcsonzes) =>
            await this.httpClient.PostAsJsonAsync("konyvtar/kolcsonzes", kolcsonzes);

        public async Task DeleteKolcsonzesAsync(int id) =>
           await this.httpClient.DeleteAsync($"konyvtar/kolcsonzes/{id}");
    }
}
