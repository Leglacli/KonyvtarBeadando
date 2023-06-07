using Konyvtar.Contracts;
using System.Net.Http;
using System;
using System.Net.Http.Json;

namespace KonyvtarosKliens.blazor.Services
{
	public class KonyvtarosService : IKonyvtarosService
	{
		private readonly HttpClient _httpClient;

		public KonyvtarosService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public Task<IEnumerable<Konyv>?> GetAllKonyvAsync() => 
			_httpClient.GetFromJsonAsync<IEnumerable<Konyv>>("konyvtar/konyvek");

		public Task<IEnumerable<Tag>?> GetAllTagAsync() =>
			_httpClient.GetFromJsonAsync<IEnumerable<Tag>>("konyvtar/tagok");

		public Task<IEnumerable<Kolcsonzes>?> GetAllKolcsonzesAsync() =>
			_httpClient.GetFromJsonAsync<IEnumerable<Kolcsonzes>>("konyvtar/kolcsonzesek");

		public Task<Konyv?> GetKonyvAsync(int id) => 
			_httpClient.GetFromJsonAsync<Konyv?>($"konyvtar/konyv/{id}");

		public Task<Tag?> GetTagAsync(int id) =>
			_httpClient.GetFromJsonAsync<Tag?>($"konyvtar/tag/{id}");

		public Task<Kolcsonzes?> GetKolcsonzesAsync(int id) =>
			_httpClient.GetFromJsonAsync<Kolcsonzes?>($"konyvtar/kolcsonzes/{id}");

		public async Task AddTagAsync(Tag tag) =>
			await _httpClient.PostAsJsonAsync("konyvtar/tag", tag);

		public async Task AddKolcsonzesAsync(Kolcsonzes kolcsonzes) =>
            await _httpClient.PostAsJsonAsync("konyvtar/kolcsonzes", kolcsonzes);

		public async Task DeleteKolcsonzesAsync(int id) =>
		   await _httpClient.DeleteAsync($"konyvtar/kolcsonzes/{id}");
	}
}
