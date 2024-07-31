using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
