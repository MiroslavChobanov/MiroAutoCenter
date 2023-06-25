using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiroAutoCenter.Core.Constants;
using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Core.Extensions;
using MiroAutoCenter.Core.Models.Admin;
using MiroAutoCenter.Core.Models.Cars;

namespace MiroAutoCenter.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IAdminService admins;
        private readonly IUserService users;
        private readonly ICarService cars;
        public AdminController(RoleManager<IdentityRole> roleManager, IAdminService admins, IUserService users, ICarService cars)
        {
            this.roleManager = roleManager;
            this.admins = admins;
            this.users = users;
            this.cars = cars;
        }

        public IActionResult Index()
        {
            return Ok();
        }

        [Authorize(Roles = UserConstants.Administrator)]
        public async Task<IActionResult> ManageUsers()
        {
            return View();
        }

        //public async Task<IActionResult> CreateRole()
        //{
        //    await roleManager.CreateAsync(new IdentityRole()
        //    {
        //        Name = "Receptionist"
        //    });

        //    return Ok();
        //}

        [Authorize(Roles = $"{UserConstants.Receptionist}")]
        public IActionResult ApproveServices(int p = 1, int s = 10)
        {
            var loggedUserId = this.users.IdByUser(this.User.Id());

            if (loggedUserId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var allMyServicesForm = this.admins.AllPendingServices(p, s);

            return View(allMyServicesForm);
        }

        [Authorize(Roles = $"{UserConstants.Receptionist}")]
        public IActionResult Approve(Guid id, AdminApproveDisapproveServiceModel service)
        {

            var approved = this.admins.Approve(id);

            if (!approved)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Успешно одобрихте заявката.";
            return RedirectToAction("ApproveServices");
        }

        [Authorize(Roles = $"{UserConstants.Receptionist}")]
        public IActionResult Disapprove(Guid id, AdminApproveDisapproveServiceModel service)
        {

            var disapproved = this.admins.Disapprove(id);

            if (!disapproved)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Успешно отхвърлихте заявката.";
            return RedirectToAction("ApproveServices");
        }

        [Authorize(Roles = $"{UserConstants.Receptionist}")]
        public IActionResult CarsWithStatus()
        {
            var myCars = this.cars.GetCarsWithStatus();

            var orderedCars = myCars
                .OrderBy(c => c.CarStatus)
                .ToList();

            return View(orderedCars);
        }
        [Authorize(Roles = UserConstants.Receptionist)]
        public IActionResult EditStatus(Guid id)
        {

            var cars = this.cars.Details(id);

            var carsForm = this.cars.EditStatusViewData(cars.Id);

            carsForm.CarStatuses = this.cars.AllCarStatuses();

            return View(carsForm);
        }
        [HttpPost]
        [Authorize(Roles = UserConstants.Receptionist)]
        public IActionResult EditStatus(Guid id, CarStatusEditModel car)
        {

            var edited = this.cars.EditStatus(
                id,
                car.CarStatusId
                );

            if (!edited)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Статусът на автомобила е редактиран успешно!";
            return RedirectToAction("CarsWithStatus");
        }

        [Authorize(Roles = UserConstants.Receptionist)]
        public IActionResult IncomingAppointments()
        {
            var approvedAppts = this.admins.GetAllApproved();

            var filteredAppts = approvedAppts
                .Where(aa => aa.Time.Date > DateTime.UtcNow.Date)
                .OrderBy(aa => aa.Time)
                .ToList();

            return View(filteredAppts);
        }

        [Authorize(Roles = UserConstants.Receptionist)]
        public IActionResult PastAppointments()
        {
            var approvedAppts = this.admins.GetAllApproved();

            var filteredAppts = approvedAppts
                .Where(aa => aa.Time.Date < DateTime.UtcNow.Date)
                .OrderBy(aa => aa.Time)
                .ToList();

            return View(filteredAppts);
        }

        [Authorize(Roles = UserConstants.Receptionist)]
        public IActionResult ReceivedCars()
        {
            var received = this.admins.GetReceivedCars();

            return View(received);
        }
    }
}
