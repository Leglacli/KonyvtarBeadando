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
	}
}
