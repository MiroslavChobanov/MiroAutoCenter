using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiroAutoCenter.Core.Constants;
using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Core.Extensions;
using MiroAutoCenter.Core.Models.Cars;
using MiroAutoCenter.Data;
using MiroAutoCenter.Data.Models;
using System.Diagnostics;
using System.Reflection;

namespace MiroAutoCenter.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService cars;
        private readonly IUserService users;
        private readonly IQueryService queryLogs;
        public CarsController(ICarService cars, IUserService users, IQueryService queryLogs)
        {
            this.cars = cars;
            this.users = users;
            this.queryLogs = queryLogs;
        }

        [Authorize]
        public IActionResult MyCars()
        { 
            var myCars = this.cars.ByUser(this.User.Id());

            return View(myCars);
        }

        //[Authorize(Roles = UserConstants.Administrator)]
        [Authorize]
        public IActionResult Add()
        {
            return View(new CarAddFormModel
            {
                CarTypes = this.cars.AllCarTypes()
            });
        }

        [HttpPost]
        //[Authorize(Roles = UserConstants.Administrator)]
        [Authorize]
        public IActionResult Add(CarAddFormModel car)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Грешка възникна!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var carId = this.cars.CreateCar(
                car.Make,
                car.Model,
                car.YearOfCreation,
                false,
                car.PlateNumber,
                car.Mileage,
                car.CarTypeId,
                userId);

            stopwatch.Stop();
            TimeSpan time = stopwatch.Elapsed;

            var query = this.queryLogs.AddNewQueryLog("Add Car", time, "Post");

            TempData[MessageConstants.SuccessMessage] = "Автомобилът е добавен успешно!";
            return RedirectToAction("MyCars");
        }

        public IActionResult Details(Guid id)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            if (id == Guid.Empty)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var cars = this.cars.Details(id);

            if (cars == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction("MyCars");
            }

            stopwatch.Stop();
            TimeSpan time = stopwatch.Elapsed;

            string methodName = MethodBase.GetCurrentMethod().Name;

            var query = this.queryLogs.AddNewQueryLog(methodName, time, "Get");

            return View(cars);
        }

        //[Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Edit(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var cars = this.cars.Details(id);

            var carsForm = this.cars.EditViewData(cars.Id);

            carsForm.CarTypes = this.cars.AllCarTypes();

            return View(carsForm);
        }

        [HttpPost]
        //[Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Edit(Guid id, CarAddFormModel car)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var edited = this.cars.Edit(
                id,
                car.Make,
                car.Model,
                car.YearOfCreation,
                car.PlateNumber,
                car.Mileage,
                car.CarTypeId
                );

            if (!edited)
            {
                return BadRequest();
            }

            stopwatch.Stop();
            TimeSpan time = stopwatch.Elapsed;

            string methodName = MethodBase.GetCurrentMethod().Name;

            var query = this.queryLogs.AddNewQueryLog("Edit car", time, "Put");

            TempData[MessageConstants.SuccessMessage] = "Автомобилът е редактиран успешно!";
            return Redirect($"../../Cars/Details/{id}");
        }

        //[Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Delete(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var car = this.cars.Details(id);

            var carForm = this.cars.DeleteViewData(car.Id);

            return View(carForm);
        }

        [HttpPost]
        //[Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Delete(Guid id, CarDeleteModel car)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var deleted = this.cars.Delete(id, true);

            if (!deleted)
            {
                return BadRequest();
            }

            stopwatch.Stop();
            TimeSpan time = stopwatch.Elapsed;

            string methodName = MethodBase.GetCurrentMethod().Name;

            var query = this.queryLogs.AddNewQueryLog("Delete car", time, "Delete");

            TempData[MessageConstants.SuccessMessage] = "Автомобилът е изтрит успешно!";
            return RedirectToAction("MyCars");
        }
    }
}
