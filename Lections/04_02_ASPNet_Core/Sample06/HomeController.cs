using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace Sample06
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("What???");
        }

        public IActionResult Hello(string name)
        {
            return Ok(GetHelloByeString("Hello", name));
        }

        public IActionResult Bye(string name)
        {
            return Ok(GetHelloByeString("Bye", name));
        }

        //[HttpGet("{word:regex(^hello|bye$)}")]
        //public IActionResult ByWord(string word, string name)
        //{
        //    return Ok(GetHelloByeString(word, name));
        //}


        private static string GetHelloByeString(string helloBye, string? name)
        {
            name = string.IsNullOrEmpty(name) ? "World" : name;
            return $"{helloBye.Transform(To.TitleCase)}, {name}\n";
        }
    }
}
