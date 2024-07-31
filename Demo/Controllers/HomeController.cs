using Demo.Language;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Demo.Controllers
{
    [Authorize(Roles ="Admin,User,Test")]
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<SharedResource> localizer;

        public HomeController(IStringLocalizer<SharedResource> localizer)
        {
            this.localizer = localizer;
        }

        //[Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            ViewBag.Msg = localizer["DASHBOARD"];
            return View();
        }
    }
}
