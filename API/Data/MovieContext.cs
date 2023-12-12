using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasOne(a => a.Cinema)
                .WithOne(c => c.Address)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Session>()
                .HasKey(s => new { s.MovieId, s.CinemaId });

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Movie)
                .WithMany(m => m.Sessions)
                .HasForeignKey(s => s.MovieId);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Cinema)
                .WithMany(c => c.Sessions)
                .HasForeignKey(s => s.CinemaId);
        }
    }
}
