using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Core.Models.Admin;
using MiroAutoCenter.Core.Models.Cars;
using MiroAutoCenter.Core.Models.Users;
using MiroAutoCenter.Data;
using MiroAutoCenter.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiroAutoCenter.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly MiroAutoCenterDbContext data;

        public AdminService(MiroAutoCenterDbContext data)
        {
            this.data = data;
        }

        public AdminPendingServicesPaginationModel AllPendingServices(int pageNo, int pageSize)
        {
            AdminPendingServicesPaginationModel result = new AdminPendingServicesPaginationModel()
            {
                PageNo = pageNo,
                PageSize = pageSize
            };

            result.TotalRecords = this.data.ServicesCars.Where(x => x.ServiceStatus.ClassColor == "table-warning").Count();
            result.AllPendingServices = this.data.ServicesCars
                .Where(x => x.ServiceStatus.ClassColor == "table-warning")
                .Select(x => new AdminAllPendingServicesModel
                {
                    Id = x.Id,
                    Time = x.Time,
                    ServiceType = x.Service.Name,
                    CarMake = x.Car.Make
                })
                 .Skip(pageNo * pageSize - pageSize)
                 .Take(pageSize)
                 .ToList();

            return result;
        }

        public bool Approve(Guid id)
        {
            var serviceCarData = this.data.ServicesCars.First(x => x.Id == id);

            var carId = serviceCarData.CarId;
            var carData = this.data.Cars.First(x => x.Id == carId);

            if (serviceCarData == null)
            {
                return false;
            }

            serviceCarData.ServiceStatusId = this.data.ServiceStatuses.First(x => x.ClassColor == "table-success").Id;
            serviceCarData.ServiceStatus = this.data.ServiceStatuses.First(x => x.ClassColor == "table-success");

            carData.CarStatusId = this.data.CarStatuses.First(x => x.StatusDescription == "Незапочната").Id;
            carData.CarStatus = this.data.CarStatuses.First(x => x.StatusDescription == "Незапочната");

            this.data.SaveChanges();

            return true;
        }

        public bool Disapprove(Guid id)
        {
            var serviceCarData = this.data.ServicesCars.First(x => x.Id == id);

            if (serviceCarData == null)
            {
                return false;
            }

            serviceCarData.ServiceStatusId = this.data.ServiceStatuses.First(x => x.ClassColor == "table-danger").Id;
            serviceCarData.ServiceStatus = this.data.ServiceStatuses.First(x => x.ClassColor == "table-danger");

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<AppointmentModel> GetAllApproved()
        {
            return this.data.ServicesCars
                .Where(a => a.ServiceStatus.ClassColor == "table-success")
                .Select(a => new AppointmentModel
                {
                    Time = a.Time,
                    ServiceName = a.Service.Name,
                    CarModel = a.Car.Make
                })
                .ToList();
        }

        public IEnumerable<CarStatusEditModel> GetReceivedCars()
        {
            return this.data.Cars
                .Where(x => !x.IsDeleted && x.CarStatus.StatusDescription == "Получен")
                .Select(v => new CarStatusEditModel
                {
                    Id = v.Id,
                    Make = v.Make,
                    Model = v.Model,
                    PlateNumber = v.PlateNumber,
                    YearOfCreation = v.YearOfCreation,
                    Mileage = v.Mileage,
                    CarStatusId = v.CarStatusId,
                    CarStatus = v.CarStatus
                })
                .ToList();
        }

        public bool AddReply(Guid questionId, string replyContent)
        {
            try
            {
                var question = this.data.Questions.FirstOrDefault(q => q.Id == questionId);

                if (question == null)
                {
                    return false;
                }

                var reply = new Reply
                {
                    QuestionId = questionId,
                    Content = replyContent,
                    CreatedAt = DateTime.Now
                };

                this.data.Replies.Add(reply);
                question.ReplyId = reply.Id;
                this.data.SaveChanges();

                return true; 
            }
            catch (Exception ex)
            {
                return false; 
            }
        }

    }
}
