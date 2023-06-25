using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiroAutoCenter.Core.Constants;
using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Core.Extensions;
using MiroAutoCenter.Core.Models.Cars;
using MiroAutoCenter.Core.Models.Ratings;
using System.Diagnostics;

namespace MiroAutoCenter.Controllers
{
    public class RatingsController : Controller
    {
        private readonly IRatingService ratings;
        private readonly IUserService users;
        public RatingsController(IRatingService ratings, IUserService users)
        {
            this.ratings = ratings;
            this.users = users;
        }

        public IActionResult All()
        {
            var ratings = this.ratings.All();

            return View(ratings);
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(RatingAddModel rating)
        {

            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var currentDate = DateTime.UtcNow;
            var username = User.Identity.Name;

            var ratingId = this.ratings.AddRating(
                rating.StarRating,
                rating.Comment,
                userId,
                currentDate
            );

            TempData[MessageConstants.SuccessMessage] = "Отзивът е добавен успешно!";
            return RedirectToAction("All");
        }
    }
}
