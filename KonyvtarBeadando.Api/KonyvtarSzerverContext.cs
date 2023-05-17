using Konyvtar.Contracts;
using Microsoft.EntityFrameworkCore;

namespace KonyvtarSzerver.Api
{
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
