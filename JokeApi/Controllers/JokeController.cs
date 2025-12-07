using Microsoft.AspNetCore.Mvc;

namespace JokeApi.Controllers
{
    public class JokeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
