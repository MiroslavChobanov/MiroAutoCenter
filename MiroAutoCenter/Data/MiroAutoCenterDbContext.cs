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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
              .Entity<Service>()
              .HasOne(x => x.ServiceStatus)
              .WithMany(x => x.Services)
              .HasForeignKey(x => x.ServiceStatusId)
              .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}