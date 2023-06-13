// <copyright file="KonyvtarSzerverContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KonyvtarSzerver.Api
{
    using Konyvtar.Contracts;
    using Microsoft.EntityFrameworkCore;

    public class KonyvtarSzerverContext : DbContext
    {
        public KonyvtarSzerverContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Konyv> Konyv { get; set; }

        public virtual DbSet<Tag> Tag { get; set; }

        public virtual DbSet<Kolcsonzes> Kolcsonzes { get; set; }
    }
}
