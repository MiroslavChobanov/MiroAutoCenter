using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiroAutoCenter.Core.Constants;

namespace MiroAutoCenter.Areas.RoleManage.Controllers
{
    [Authorize(Roles = UserConstants.Administrator)]
    [Area("RoleManage")]
    public class BaseController : Controller
    {

    }
}
