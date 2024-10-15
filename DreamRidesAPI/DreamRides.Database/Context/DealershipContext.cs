using DreamRides.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace DreamRides.Database.Context
{
    public class DealershipContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        
        public DealershipContext(DbContextOptions<DealershipContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=ITRO-54;initial catalog=DreamRides;Trusted_Connection=True;trust server certificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            
            modelBuilder.Entity<Favorite>()
                .HasKey(f => new { f.UserId, f.CarId });

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Car)
                .WithMany(c => c.Favorites)
                .HasForeignKey(f => f.CarId);
        }

    }

}
