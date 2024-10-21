using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ST_AI_MusicShop2.Models
{
    public class MusicShopDbContext : DbContext
    {
        public MusicShopDbContext(DbContextOptions<MusicShopDbContext> options) : base(options) { }

        public DbSet<Album> Albums { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CD>();
            builder.Entity<Vinyl>();
            builder.Entity<DigitalAlbum>();

            //builder.Entity<Album>()
            //    .HasDiscriminator<string>("AlbumType")
            //    .HasValue<CD>("CD")
            //    .HasValue<Vinyl>("Vinyl")
            //    .HasValue<DigitalAlbum>("DigitalAlbum");

            base.OnModelCreating(builder);

        }
    }
}
