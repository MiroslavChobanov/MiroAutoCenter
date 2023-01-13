using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiroAutoCenter.Core.Contracts;

namespace MiroAutoCenter.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService users;

        public UserController(IUserService users)
        {
            this.users = users;
        }

        [Authorize]
        public IActionResult MyProfile()
        {
            return View();
        }

        public IActionResult UserProfile(string id)
        {

            var detailsForm = this.users.UserDetails(id);

            return View(detailsForm);
        }

        [Authorize]
        public IActionResult Chat()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
