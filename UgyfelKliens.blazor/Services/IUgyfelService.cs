// <copyright file="IUgyfelService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UgyfelKliens.Blazor.Services
{
    using Konyvtar.Contracts;

    public interface IUgyfelService
    {
        Task<IEnumerable<Konyv>?> GetAllKonyvAsync();

        Task<IEnumerable<Kolcsonzes>?> GetAllKolcsonzesAsync();

        Task<IEnumerable<Tag>?> GetAllTagAsync();

        Task<Konyv?> GetKonyvAsync(int id);

        Task<Tag?> GetTagAsync(int id);

        Task<Kolcsonzes?> GetKolcsonzesAsync(int id);

        Task AddTagAsync(Tag tag);

        Task AddKolcsonzesAsync(Kolcsonzes kolcsonzes);

        Task DeleteKolcsonzesAsync(int id);
    }
}
