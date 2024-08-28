using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PapaSmurfie.Utility;

namespace PapaSmurfie.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
