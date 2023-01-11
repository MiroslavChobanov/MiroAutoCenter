using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Core.Models.Cars;
using MiroAutoCenter.Core.Models.Services;
using MiroAutoCenter.Data;
using MiroAutoCenter.Data.Models;
using System.Xml.Linq;

namespace MiroAutoCenter.Core.Services
{
    public class ServiceService : IServiceService
    {
        private readonly MiroAutoCenterDbContext data;

        public ServiceService(MiroAutoCenterDbContext data)
        {
            this.data = data;
        }
        public List<ServicesListingModel> All()
        {
            return this.data
                 .Services
                 .Where(x => !x.IsDeleted)
                 .Select(x => new ServicesListingModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Description = x.Description,
                     Price= x.Price,
                     Time = x.Time,
                 })
                 .OrderBy(x => x.Id)
                 .ToList();
        }
        public Guid CreateService(
            string name,
            string description,
            decimal price,
            string time)
        {
            var newService = new Service
            {
                Name = name,
                Description = description,
                Price = price,
                Time = time
            };

            this.data.Services.Add(newService);
            this.data.SaveChanges();

            return newService.Id;
        }

        public ServicesListingModel Details(Guid id)
        {
            return this.data.Services
                .Where(x => x.Id == id)
                .Select(x => new ServicesListingModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Time = x.Time
                })
                .First();
        }

        public bool Edit(
            Guid id,
            string name,
            string description,
            decimal price,
            string time)
        {
            var serviceData = this.data.Services.Find(id);

            if (serviceData == null)
            {
                return false;
            }

            serviceData.Name = name;
            serviceData.Description = description;
            serviceData.Price = price;
            serviceData.Time = time;


            this.data.SaveChanges();

            return true;
        }

        public bool Delete(Guid id, bool isDeleted)
        {
            var serviceData = this.data.Services.Find(id);

            if (serviceData == null)
            {
                return false;
            }

            serviceData.IsDeleted = isDeleted;

            this.data.SaveChanges();

            return true;
        }

        public ServiceDeleteModel DeleteViewData(Guid id)
        {
            return this.data.Services
                .Where(x => x.Id == id)
                .Select(x => new ServiceDeleteModel
                {
                    Name = x.Name
                })
                .First();
        }

        public ServiceAddFormModel EditViewData(Guid id)
        {
            return this.data.Services
                .Where(x => x.Id == id)
                .Select(x => new ServiceAddFormModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Time = x.Time
                })
                .First();
        }

        public Guid CreateAppointment(Guid serviceId, Guid carId, DateTime time)
        {

            var serviceStatusId = this.data.ServiceStatuses
                .Where(ss => ss.StatusDescription == "Waiting for approval")
                .Select(ss => ss.Id)
                .First();

            var newAppointment = new ServiceCar
            {
                ServiceId = serviceId,
                CarId = carId,
                ServiceStatusId = serviceStatusId,
                Time = time
            };

            this.data.ServicesCars.Add(newAppointment);
            this.data.SaveChanges();

            return newAppointment.Id;
        }
    }
}
