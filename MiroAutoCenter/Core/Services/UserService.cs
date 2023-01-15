using Microsoft.AspNetCore.Server.IIS.Core;
using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Core.Models.Cars;
using MiroAutoCenter.Core.Models.Users;
using MiroAutoCenter.Data;
using MiroAutoCenter.Data.Models;

namespace MiroAutoCenter.Core.Services
{
    public class UserService : IUserService
    {
        private readonly MiroAutoCenterDbContext data;

        public UserService(MiroAutoCenterDbContext data)
        {
            this.data = data;
        }

        public string IdByUser(string userId)
        {
            return this.data.WebsiteUsers
                .Where(x => x.Id == userId)
                .Select(x => x.Id)
                .First();
        }

        public UserDetailsModel UserDetails(string id)
        {
            return this.data.WebsiteUsers
                .Where(x => x.Id == id)
                .Select(x => new UserDetailsModel
                {
                    Id = id,
                    Email = x.Email,
                    Username = x.UserName,
                    Role = this.data.Roles.First(x => x.Id == this.data.UserRoles.First(x => x.UserId == id).RoleId).Name
                })
                .First();
        }

        public IEnumerable<AppointmentModel> UserApproved(string userId)
        {

            return GetApproved(this.data
                .ServicesCars
                .Where(c => c.Car.UserId == userId));
        }

        private IEnumerable<AppointmentModel> GetApproved(IQueryable<ServiceCar> apptQuery)
        {
            return apptQuery
                .Where(a => a.ServiceStatus.ClassColor == "table-success")
                .Select(a => new AppointmentModel
                {
                    Time = a.Time,
                    ServiceName = a.Service.Name,
                    CarModel = a.Car.Make
                })
                .ToList();
        }

        public IEnumerable<AppointmentModel> UserRejected(string userId)
        {

            return GetRejected(this.data
                .ServicesCars
                .Where(c => c.Car.UserId == userId));
        }

        private IEnumerable<AppointmentModel> GetRejected(IQueryable<ServiceCar> apptQuery)
        {
            return apptQuery
                .Where(a => a.ServiceStatus.ClassColor == "table-danger")
                .Select(a => new AppointmentModel
                {
                    Time = a.Time,
                    ServiceName = a.Service.Name,
                    CarModel = a.Car.Make
                })
                .ToList();
        }

        public IEnumerable<AppointmentModel> UserWaiting(string userId)
        {

            return GetWaiting(this.data
                .ServicesCars
                .Where(c => c.Car.UserId == userId));
        }

        private IEnumerable<AppointmentModel> GetWaiting(IQueryable<ServiceCar> apptQuery)
        {
            return apptQuery
                .Where(a => a.ServiceStatus.ClassColor == "table-warning")
                .Select(a => new AppointmentModel
                {
                    Time = a.Time,
                    ServiceName = a.Service.Name,
                    CarModel = a.Car.Make
                })
                .ToList();
        }

    }
}
