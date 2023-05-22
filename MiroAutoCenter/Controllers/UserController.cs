using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Core.Extensions;

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

        public IActionResult Tips()
        {
            return View();
        }

        [Authorize]
        public IActionResult ApprovedAppointments()
        {
            var approvedAppts = this.users.UserApproved(this.User.Id());

            var filteredAppts = approvedAppts
                .Where(aa => aa.Time.Date > DateTime.UtcNow.Date)
                .ToList();

            return View(filteredAppts);
        }

        [Authorize]
        public IActionResult RejectedAppointments()
        {
            var rejectedAppts = this.users.UserRejected(this.User.Id());

            var filteredAppts = rejectedAppts
                .Where(aa => aa.Time.Date > DateTime.UtcNow.Date)
                .ToList();

            return View(filteredAppts);
        }

        [Authorize]
        public IActionResult WaitingForApproval()
        {
            var waitingAppts = this.users.UserWaiting(this.User.Id());

            var filteredAppts = waitingAppts
                .Where(aa => aa.Time.Date > DateTime.UtcNow.Date)
                .ToList();

            return View(filteredAppts);
        }

        [Authorize]
        public IActionResult History()
        {
            var approvedAppts = this.users.UserApproved(this.User.Id());

            var filteredAppts = approvedAppts
                .Where(aa => aa.Time.Date < DateTime.UtcNow.Date)
                .ToList();

            return View(filteredAppts);
        }
    }
}
