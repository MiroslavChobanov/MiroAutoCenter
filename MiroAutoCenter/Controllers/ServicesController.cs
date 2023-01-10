using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiroAutoCenter.Core.Constants;
using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Core.Extensions;
using MiroAutoCenter.Core.Models.Cars;
using MiroAutoCenter.Core.Models.Services;
using System.Data;

namespace MiroAutoCenter.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceService services;
        private readonly IUserService users;
        public ServicesController(IServiceService services, IUserService users)
        {
            this.services = services;
            this.users = users;
        }

        public IActionResult All()
        {
            var services = this.services.All();

            return View(services);
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
                TempData[MessageConstants.ErrorMessage] = "An error occurred!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var serviceId = this.services.CreateService(
                service.Name,
                service.Description,
                service.Price,
                service.Time
                );

            TempData[MessageConstants.SuccessMessage] = "The service was successfully added.";
            return RedirectToAction("All");
        }

        public IActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData[MessageConstants.ErrorMessage] = "An error occurred!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var services = this.services.Details(id);

            if (services == null)
            {
                TempData[MessageConstants.ErrorMessage] = "An error occurred!";
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
                TempData[MessageConstants.ErrorMessage] = "An error occurred!";
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

            TempData[MessageConstants.SuccessMessage] = "Service has been successfully edited!";
            return Redirect($"../../Services/Details/{id}");
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

            TempData[MessageConstants.SuccessMessage] = "Service has been successfully deleted.";
            return RedirectToAction("All");
        }
    }
}
