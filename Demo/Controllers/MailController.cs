using Demo.BL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
namespace Demo.Controllers
{
    public class MailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(MailVM model)
        {
            try
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("aboalshikh56@gmail.com", "XXxx22##");
                smtp.Send("aboalshikh56@gmail.com", "aboalshikh56@gmail.com", model.Title, model.Message);

                TempData["msg"] = "Mail send successfully";
                return View();
            }
            catch
            {
                TempData["msg"] = "failed";
                return View();
            }
        }
    }
}
