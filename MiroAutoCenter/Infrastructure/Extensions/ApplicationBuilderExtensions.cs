using MiroAutoCenter.Data;
using MiroAutoCenter.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MiroAutoCenter.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedServiceStatuses(services);
            SeedCarTypes(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<MiroAutoCenterDbContext>();

            data.Database.Migrate();
        }

        private static void SeedServiceStatuses(IServiceProvider services)
        {
            var data = services.GetRequiredService<MiroAutoCenterDbContext>();

            if (data.ServiceStatuses.Any())
            {
                return;
            }

            data.ServiceStatuses.AddRange(new[]
            {
                new ServiceStatus { StatusDescription = "Approved query", ClassColor="table-success", Counter = 1},
                new ServiceStatus { StatusDescription = "Waiting for approval", ClassColor="table-warning", Counter = 2},
                new ServiceStatus { StatusDescription = "Rejected query", ClassColor="table-danger", Counter = 3},

            });

            data.SaveChanges();
        }

        private static void SeedCarTypes(IServiceProvider services)
        {
            var data = services.GetRequiredService<MiroAutoCenterDbContext>();

            if (data.CarTypes.Any())
            {
                return;
            }

            data.CarTypes.AddRange(new[]
            {
                new CarType { Type = "Sedan" },
                new CarType { Type = "Coupe" },
                new CarType { Type = "Sports car" },
                new CarType { Type = "Station Wagon" },
                new CarType { Type = "Hatchback" },
                new CarType { Type = "Convertible" },
                new CarType { Type = "SUV" },
                new CarType { Type = "Pickup Truck" }
            });

            data.SaveChanges();
        }
    }
}
