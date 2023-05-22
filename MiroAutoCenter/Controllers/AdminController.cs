using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiroAutoCenter.Core.Constants;
using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Core.Extensions;
using MiroAutoCenter.Core.Models.Admin;

namespace MiroAutoCenter.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IAdminService admins;
        private readonly IUserService users;
        public AdminController(RoleManager<IdentityRole> roleManager, IAdminService admins, IUserService users)
        {
            this.roleManager = roleManager;
            this.admins = admins;
            this.users = users;
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

            TempData[MessageConstants.SuccessMessage] = "Успешно отвхърлихте заявката.";
            return RedirectToAction("ApproveServices");
        }
    }
}
