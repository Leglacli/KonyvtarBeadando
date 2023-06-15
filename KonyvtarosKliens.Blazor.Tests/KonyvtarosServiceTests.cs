namespace KonyvtarosKliens.Blazor.Tests
{
	using Moq;
	using System;
	using System.Net;
	using System.Net.Http;
	using Konyvtar.Contracts;
	using KonyvtarosKliens.Blazor.Services;
	using Moq.Protected;
	using System.Net.Http.Formatting;

	[TestClass]
	public class KonyvtarosServiceTests
	{
		private IKonyvtarosService konyvtarosService;
		private HttpClient httpClient;
		private Mock<HttpMessageHandler> httpMessageHandlerMock;

		private void Setup()
		{
			this.httpMessageHandlerMock = new Mock<HttpMessageHandler>();
			this.httpClient = new HttpClient(this.httpMessageHandlerMock.Object);
			this.httpClient.BaseAddress = new Uri("https://localhost:7215");
			this.konyvtarosService = new KonyvtarosService(this.httpClient);
		}

		[TestMethod]
		public async Task GetKonyvAsync_WithValidId_ReturnsKonyv()
		{
			// Arrange
			Setup();
			int konyvLeltariSzam = 3;
			string konyvCim = "1984";
			string konyvSzerzo = "George Orwell";
			string konyvKiado = "Harvill Secker";
			DateTime konyvKiadasEve = DateTime.Parse("1949-06-08");
			Konyv expectedKonyv = new Konyv { LeltariSzam = konyvLeltariSzam, Cim = konyvCim, Szerzo = konyvSzerzo, Kiado = konyvKiado, KiadasEve = konyvKiadasEve };

			this.httpMessageHandlerMock.Protected()
				.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(new HttpResponseMessage
				{
					StatusCode = HttpStatusCode.OK,
					Content = new ObjectContent<Konyv>(expectedKonyv, new JsonMediaTypeFormatter())
				});

			// Act
			var result = await this.konyvtarosService.GetKonyvAsync(konyvLeltariSzam);

			// Assert
			Assert.AreEqual(expectedKonyv.LeltariSzam, result.LeltariSzam);
			Assert.AreEqual(expectedKonyv.Cim, result.Cim);
			Assert.AreEqual(expectedKonyv.Szerzo, result.Szerzo);
			Assert.AreEqual(expectedKonyv.Kiado, result.Kiado);
			Assert.AreEqual(expectedKonyv.KiadasEve, result.KiadasEve);
		}

		[TestMethod]
		public async Task GetTagAsync_WithValidId_ReturnsTag()
		{
			// Arrange
			Setup();
			int tagOlvasoSzam = 2;
			string tagNev = "Teszt Elek";
			string tagLakcim = "Budapest, 412";
			DateTime tagSzuletesiDatum = DateTime.Parse("1980-05-12");
			Tag expectedTag = new Tag { OlvasoSzam = tagOlvasoSzam, Nev = tagNev, Lakcim = tagLakcim, SzuletesiDatum = tagSzuletesiDatum };

			this.httpMessageHandlerMock.Protected()
				.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(new HttpResponseMessage
				{
					StatusCode = HttpStatusCode.OK,
					Content = new ObjectContent<Tag>(expectedTag, new JsonMediaTypeFormatter())
				});

			// Act
			var result = await this.konyvtarosService.GetTagAsync(tagOlvasoSzam);

			// Assert
			Assert.AreEqual(expectedTag.OlvasoSzam, result.OlvasoSzam);
			Assert.AreEqual(expectedTag.Nev, result.Nev);
			Assert.AreEqual(expectedTag.Lakcim, result.Lakcim);
			Assert.AreEqual(expectedTag.SzuletesiDatum, result.SzuletesiDatum);
		}

		[TestMethod]
		public async Task GetKolcsonzesAsync_WithValidId_ReturnsKolcsonzes()
		{
			// Arrange
			Setup();
			int kolcsonzesId = 10;
			int kolcsonzesOlvasoSzam = 2;
			int kolcsonzesLeltariSzam = 5;
			DateTime kolcsonzesKolcsonzesIdeje = DateTime.Parse("2023-03-06");
			DateTime kolcsonzesVisszahozasIdeje = DateTime.Parse("2023-03-10");
			Kolcsonzes expectedKolcsonzes = new Kolcsonzes { Id = kolcsonzesId, OlvasoSzam = kolcsonzesOlvasoSzam, LeltariSzam = kolcsonzesLeltariSzam, KolcsonzesIdeje = kolcsonzesKolcsonzesIdeje, VisszahozasIdeje = kolcsonzesVisszahozasIdeje };
			this.httpMessageHandlerMock.Protected()
				.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(new HttpResponseMessage
				{
					StatusCode = HttpStatusCode.OK,
					Content = new ObjectContent<Kolcsonzes>(expectedKolcsonzes, new JsonMediaTypeFormatter())
				});

			// Act
			var result = await this.konyvtarosService.GetKolcsonzesAsync(kolcsonzesId);

			// Assert
			Assert.AreEqual(expectedKolcsonzes.Id, result.Id);
			Assert.AreEqual(expectedKolcsonzes.OlvasoSzam, result.OlvasoSzam);
			Assert.AreEqual(expectedKolcsonzes.LeltariSzam, result.LeltariSzam);
			Assert.AreEqual(expectedKolcsonzes.KolcsonzesIdeje, result.KolcsonzesIdeje);
			Assert.AreEqual(expectedKolcsonzes.VisszahozasIdeje, result.VisszahozasIdeje);
		}
	}
}