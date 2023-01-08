using Microsoft.AspNetCore.Mvc;

namespace MiroAutoCenter.Areas.RoleManage.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
