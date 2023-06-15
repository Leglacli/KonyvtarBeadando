// <copyright file="IKonyvtarosService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KonyvtarosKliens.Blazor.Services
{
    using Konyvtar.Contracts;

    public interface IKonyvtarosService
    {
        /// <summary>
        /// Retrieves all Konyv objects asynchronously.
        /// </summary>
        /// <returns>A task result which contains the collection of retrieved Konyv objects if found, otherwise it returns null.</returns>
        Task<IEnumerable<Konyv>?> GetAllKonyvAsync();

        /// <summary>
        /// Retrieves all Tag objects asynchronously.
        /// </summary>
        /// <returns>A task result which contains the collection of retrieved Tag objects if found, otherwise it returns null.</returns>
        Task<IEnumerable<Kolcsonzes>?> GetAllKolcsonzesAsync();

        /// <summary>
        /// Retrieves all Kolcsonzes objects asynchronously.
        /// </summary>
        /// <returns>A task result which contains the collection of retrieved Kolcsonzes objects if found, otherwise it returns null.</returns>
        Task<IEnumerable<Tag>?> GetAllTagAsync();

        /// <summary>
        /// Retrieves a Konyv object asynchronously based on the specified leltari szam(ID).
        /// </summary>
        /// <param name="leltariSzam">The ID of the Konyv object to retrieve.</param>
        /// <returns>A task result which contains the retrieved Konyv object if found, otherwise it returns null.</returns>
        Task<Konyv?> GetKonyvAsync(int leltariSzam);

        /// <summary>
        /// Retrieves a Tag object asynchronously based on the specified olvaso szam(ID).
        /// </summary>
        /// <param name="olvasoSzam">The ID of the Tag object to retrieve.</param>
        /// <returns>A task result which contains the retrieved Tag object if found, otherwise it returns null.</returns>
        Task<Tag?> GetTagAsync(int olvasoSzam);

        /// <summary>
        /// Retrieves a Kolcsonzes object asynchronously based on the specified ID.
        /// </summary>
        /// <param name="id">The ID of the Kolcsonzes object to retrieve.</param>
        /// <returns>A task result which contains the retrieved Kolcsonzes object if found, otherwise it returns null.</returns>
        Task<Kolcsonzes?> GetKolcsonzesAsync(int id);

        /// <summary>
        /// Adds a new Konyv asynchronously.
        /// </summary>
        /// <param name="konyv">The Konyv object to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddKonyvAsync(Konyv konyv);

        /// <summary>
        /// Adds a new Tag asynchronously.
        /// </summary>
        /// <param name="tag">The Tag object to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddTagAsync(Tag tag);

        /// <summary>
        /// Adds a new Kolcsonzes asynchronously.
        /// </summary>
        /// <param name="kolcsonzes">The Kolcsonzes object to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddKolcsonzesAsync(Kolcsonzes kolcsonzes);

        /// <summary>
        /// Deletes a Konyv asynchronously based on the specified leltari szam(ID).
        /// </summary>
        /// <param name="leltariSzam">The leltari szam(ID) of the Konyv to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteKonyvAsync(int leltariSzam);

        /// <summary>
        /// Deletes a Tag asynchronously based on the specified olvaso szam(ID).
        /// </summary>
        /// <param name="olvasoSzam">The olvaso szam(ID) of the Tag to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteTagAsync(int olvasoSzam);

        /// <summary>
        /// Deletes a Kolcsonzes asynchronously based on the specified ID.
        /// </summary>
        /// <param name="id">The ID of the Kolcsonzes to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteKolcsonzesAsync(int id);
    }
}
