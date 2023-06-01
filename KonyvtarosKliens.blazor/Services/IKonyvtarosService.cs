using Konyvtar.Contracts;

namespace KonyvtarosKliens.blazor.Services
{
	public interface IKonyvtarosService
	{
		Task<IEnumerable<Konyv>?> GetAllKonyvAsync();

		Task<IEnumerable<Kolcsonzes>?> GetAllKolcsonzesAsync();

		Task<IEnumerable<Tag>?> GetAllTagAsync();

		Task<Konyv?> GetKonyvAsync(int id);

		Task<Tag?> GetTagAsync(int id);
	}
}
