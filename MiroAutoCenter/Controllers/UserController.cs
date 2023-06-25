using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiroAutoCenter.Core.Constants;
using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Core.Extensions;
using MiroAutoCenter.Core.Models.Questions;
using MiroAutoCenter.Core.Models.Ratings;

namespace MiroAutoCenter.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService users;
        private readonly IQuestionService questions;

        public UserController(IUserService users, IQuestionService questions)
        {
            this.users = users;
            this.questions = questions;
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

        public IActionResult Ratings()
        {
            return View();
        }

        public IActionResult Questions()
        {
            var allQuestions = this.questions.All();

            return View(allQuestions);
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

        [Authorize]
        public IActionResult AddQuestion()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddQuestion(QuestionsListingViewModel question)
        {

            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var currentDate = DateTime.UtcNow;
            var username = User.Identity.Name;

            var questionId = this.questions.AddQuestion(
                question.Content,
                userId
            );

            TempData[MessageConstants.SuccessMessage] = "Въпросът е добавен успешно!";
            return RedirectToAction("Questions");
        }
    }
}
