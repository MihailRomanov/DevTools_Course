using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Sample06.Models;

namespace Sample06.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Hello(string name)
        {
            return View(new HelloViewModel { Name = name.Transform(To.TitleCase) });
        }

        public IActionResult Bye(string name)
        {
            return View(new HelloViewModel { Name = name.Transform(To.TitleCase) });
        }
    }
}
