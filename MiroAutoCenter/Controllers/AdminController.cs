using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiroAutoCenter.Core.Constants;
using MiroAutoCenter.Core.Contracts;

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
    }
}
