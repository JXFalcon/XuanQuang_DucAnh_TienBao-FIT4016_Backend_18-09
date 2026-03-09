using Microsoft.AspNetCore.Mvc;

namespace Web_BongDa_Login.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
