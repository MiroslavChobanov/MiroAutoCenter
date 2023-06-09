using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiroAutoCenter.Data.Models;

namespace MiroAutoCenter.Data
{
    public class MiroAutoCenterDbContext : IdentityDbContext
    {
        public MiroAutoCenterDbContext(DbContextOptions<MiroAutoCenterDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebsiteUser> WebsiteUsers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceStatus> ServiceStatuses { get; set; }
        public DbSet<ServiceCar> ServicesCars { get; set; }
        public DbSet<QueryLog> QueryLogs { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<CarStatus> CarStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
              .Entity<ServiceCar>()
              .HasOne(sc => sc.ServiceStatus)
              .WithMany(sc => sc.ServicesCars)
              .HasForeignKey(sc => sc.ServiceStatusId)
              .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<ServiceCar>()
                .HasKey(sc => new { sc.CarId, sc.ServiceId });
            builder
                .Entity<ServiceCar>()
                .HasOne(sc => sc.Car)
                .WithMany(c => c.ServicesCars)
                .HasForeignKey(sc => sc.CarId);
            builder
                .Entity<ServiceCar>()
                .HasOne(sc => sc.Service)
                .WithMany(s => s.ServicesCars)
                .HasForeignKey(sc => sc.ServiceId);

            base.OnModelCreating(builder);
        }
    }
}