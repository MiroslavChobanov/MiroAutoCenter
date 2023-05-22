using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiroAutoCenter.Core.Constants;
using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Core.Extensions;
using MiroAutoCenter.Core.Models.Cars;
using MiroAutoCenter.Core.Models.Services;
using MiroAutoCenter.Core.Models.ServicesCars;
using MiroAutoCenter.Data;
using MiroAutoCenter.Data.Models;
using System.Data;

namespace MiroAutoCenter.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceService services;
        private readonly IUserService users;
        private readonly ICarService cars;
        private readonly MiroAutoCenterDbContext data;
        public ServicesController(IServiceService services, IUserService users, ICarService cars, MiroAutoCenterDbContext data)
        {
            this.services = services;
            this.users = users;
            this.cars = cars;
            this.data = data;
        }

        public IActionResult All(int p = 1, int s = 10)
        {
            //var services = this.services.All();

            //return View(services);

            var allMyServicesForm = this.services.AllServices(p, s);

            return View(allMyServicesForm);
        }

        [Authorize(Roles = UserConstants.Administrator)]
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = UserConstants.Administrator)]
        [Authorize]
        public IActionResult Add(ServiceAddFormModel service)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var serviceId = this.services.CreateService(
                service.Name,
                service.Description,
                service.Price,
                service.Time
                );

            TempData[MessageConstants.SuccessMessage] = "Услугата е добавена успешно!";
            return RedirectToAction("All");
        }

        public IActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var services = this.services.Details(id);

            if (services == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction("All");
            }

            return View(services);
        }

        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Edit(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var services = this.services.Details(id);

            var servicesForm = this.services.EditViewData(services.Id);


            return View(servicesForm);
        }

        [HttpPost]
        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Edit(Guid id, ServiceAddFormModel service)
        {

            var edited = this.services.Edit(
                id,
                service.Name,
                service.Description,
                service.Price,
                service.Time
                );

            if (!edited)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Услугата е редактирана успешно!";
            return Redirect($"../../Services/Details/{id}");
        }

        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Delete(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var service = this.services.Details(id);

            var serviceForm = this.services.DeleteViewData(service.Id);

            return View(serviceForm);
        }

        [HttpPost]
        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Delete(Guid id, ServiceDeleteModel service)
        {

            var deleted = this.services.Delete(id, true);

            if (!deleted)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Услугата е успешно изтрита!";
            return RedirectToAction("All");
        }

        [Authorize]
        public IActionResult CreateAppointment(Guid id)
        {
            var dateFormatted = DateTime.Now.ToString("g");

            return View(new ServiceCarAddFormModel
            {
                UserCars = this.cars.ByUser(this.User.Id()),
                Time = DateTime.Parse(dateFormatted)
            });
        }

        //public IActionResult CreateAppointment(Guid id)
        //{
        //    var dateFormatted = DateTime.Now.ToString("g");

        //    var model = new ServiceCarAddFormModel
        //    {
        //        UserCars = this.cars.ByUser(this.User.Id()),
        //        Time = DateTime.Parse(dateFormatted)
        //    };

        //    var serviceDuration = GetServiceDuration(id); // Retrieve the duration of the selected service

        //    // Calculate the start and end dates for the week
        //    var startDate = DateTime.Today.StartOfWeek(DayOfWeek.Monday);
        //    var endDate = startDate.AddDays(4); // Assuming Monday to Friday schedule

        //    var availableDays = new List<AppointmentViewModel>();

        //    // Generate appointment data for each day in the week
        //    for (var date = startDate; date <= endDate; date = date.AddDays(1))
        //    {
        //        var dayOfWeek = date.ToString("dddd");
        //        var availableHours = GenerateAvailableHoursForDay(date, serviceDuration);

        //        var appointment = new AppointmentViewModel
        //        {
        //            DayOfWeek = dayOfWeek,
        //            AvailableHours = availableHours,
        //            ServiceDuration = serviceDuration
        //        };

        //        availableDays.Add(appointment);
        //    }

        //    model.AvailableDays = availableDays;

        //    return View(model);
        //}


        //    private List<AppointmentHour> GenerateAvailableHoursForDay(DateTime date, int serviceDuration)
        //    {
        //        // Logic to generate the available hours for the given day
        //        // Update this logic based on your requirements and data source
        //        // Return a list of AppointmentHour objects with the appropriate IsAvailable property
        //        // Set based on the service duration and other conditions

        //        var availableHours = new List<AppointmentHour>
        //{
        //    new AppointmentHour { Hour = "9:00 AM", IsAvailable = true },
        //    new AppointmentHour { Hour = "10:00 AM", IsAvailable = true },
        //    new AppointmentHour { Hour = "11:00 AM", IsAvailable = true },
        //    new AppointmentHour { Hour = "12:00 AM", IsAvailable = true },
        //    new AppointmentHour { Hour = "13:00 AM", IsAvailable = true },
        //    new AppointmentHour { Hour = "14:00 AM", IsAvailable = true },
        //    new AppointmentHour { Hour = "15:00 AM", IsAvailable = true },
        //    new AppointmentHour { Hour = "16:00 AM", IsAvailable = true },
        //    new AppointmentHour { Hour = "17:00 AM", IsAvailable = true },
        //    new AppointmentHour { Hour = "18:00 AM", IsAvailable = true },
        //    // Add more hours as needed
        //};

        //        return availableHours;
        //    }

        //private int GetServiceDuration(Guid serviceId)
        //{
        //    // Retrieve the service from your data source based on the serviceId
        //    var service = this.services.GetServiceById(serviceId);

        //    // Return the duration of the service
        //    var duration = int.Parse(service.Time.Split(' ')[0]);
        //    return duration;
        //}



        [HttpPost]
        [Authorize]
        public IActionResult CreateAppointment(Guid id, ServiceCarAddFormModel serviceCar, string selectedTime)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            if (serviceCar.Time < DateTime.Now)
            {
                TempData[MessageConstants.ErrorMessage] = "Избрахте дата в миналото!";
                return RedirectToAction("CreateAppointment");
            }

            var serviceId = this.services.CreateAppointment(
                id,
                serviceCar.CarId,
                serviceCar.Time
                );

            //var carId = serviceCar.CarId;

            ////// Parse the selected appointment time
            ////var selectedTimeValue = DateTime.Parse(selectedTime);

            //var serviceId = this.services.CreateAppointment(
            //    id,
            //    carId,
            //    selectedTimeValue
            //);

            TempData[MessageConstants.SuccessMessage] = "Успешно направена заявка.";
            return RedirectToAction("All");
        }
        //[Authorize]
        //[HttpPost]
        //public IActionResult CreateAppointment(Guid id, ServiceCarAddFormModel serviceCar, string selectedTime)
        //{
        //    var userId = this.users.IdByUser(this.User.Id());

        //    if (userId == null)
        //    {
        //        TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
        //        return RedirectToAction(nameof(HomeController.Index), "Home");
        //    }

        //    if (serviceCar.Time < DateTime.Now)
        //    {
        //        TempData[MessageConstants.ErrorMessage] = "Избрахте дата в миналото!";
        //        return RedirectToAction("CreateAppointment");
        //    }

        //    // Retrieve the selected car ID from the form
        //    var carId = serviceCar.CarId;

        //    // Parse the selected appointment time
        //    var selectedTimeValue = DateTime.Parse(selectedTime);

        //    var serviceId = this.services.CreateAppointment(
        //        id,
        //        carId,
        //        selectedTimeValue
        //    );

        //    TempData[MessageConstants.SuccessMessage] = "Успешно направена заявка.";
        //    return RedirectToAction("All");
        //}

    }
}
