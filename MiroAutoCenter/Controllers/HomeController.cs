using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MiroAutoCenter.Core.Constants;
using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Models;
using System.Diagnostics;

namespace MiroAutoCenter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache cache;
        private readonly IRatingService ratings;

        public HomeController(ILogger<HomeController> logger,IMemoryCache cache, IRatingService ratings)
        {
            _logger = logger;
            this.cache = cache;
            this.ratings = ratings;
        }

        public IActionResult Index()
        {
            ViewData[MessageConstants.SuccessMessage] = "Добре дошли!";

            var ratings = this.ratings.All();

            return View(ratings);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}