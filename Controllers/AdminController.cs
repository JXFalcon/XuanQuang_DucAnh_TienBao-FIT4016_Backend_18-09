using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Web_BongDa_Login.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
