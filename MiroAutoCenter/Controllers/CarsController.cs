using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiroAutoCenter.Core.Constants;
using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Core.Extensions;
using MiroAutoCenter.Core.Models.Cars;

namespace MiroAutoCenter.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService cars;
        private readonly IUserService users;
        public CarsController(ICarService cars, IUserService users)
        {
            this.cars = cars;
            this.users = users;
        }

        public IActionResult All()
        {
            var cars = this.cars.All();

            return View(cars);
        }

        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Add()
        {
            return View(new CarAddFormModel
            {
                CarTypes = this.cars.AllCarTypes()
            });
        }

        [HttpPost]
        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Add(CarAddFormModel car)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "An error occurred!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var carId = this.cars.CreateCar(
                car.Make,
                car.Model,
                car.YearOfCreation,
                false,
                car.PlateNumber,
                car.Mileage,
                car.CarTypeId);

            TempData[MessageConstants.SuccessMessage] = "The car was successfully added.";
            return RedirectToAction("All");
        }

        public IActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData[MessageConstants.ErrorMessage] = "An error occurred!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var cars = this.cars.Details(id);

            if (cars == null)
            {
                TempData[MessageConstants.ErrorMessage] = "An error occurred!";
                return RedirectToAction("All");
            }

            return View(cars);
        }

        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Edit(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "An error occurred!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var cars = this.cars.Details(id);

            var carsForm = this.cars.EditViewData(cars.Id);

            carsForm.CarTypes = this.cars.AllCarTypes();

            return View(carsForm);
        }

        [HttpPost]
        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Edit(Guid id, CarAddFormModel car)
        {

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

            TempData[MessageConstants.SuccessMessage] = "Car has been successfully edited!";
            return Redirect($"../../Cars/Details/{id}");
        }

        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Delete(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "An error occurred!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var car = this.cars.Details(id);

            var carForm = this.cars.DeleteViewData(car.Id);

            return View(carForm);
        }

        [HttpPost]
        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Delete(Guid id, CarDeleteModel car)
        {

            var deleted = this.cars.Delete(id, true);

            if (!deleted)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Car has been successfully deleted.";
            return RedirectToAction("All");
        }
    }
}
