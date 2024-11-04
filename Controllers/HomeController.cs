using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Test()
        {
            try
            {
                // throw new Exception();

                ViewBag.Message = "Merhaba";
                return View();
            }
            catch
            {
                return PartialView("Hata");
            }
        }
    }
}
