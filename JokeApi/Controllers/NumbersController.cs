using Microsoft.AspNetCore.Mvc;

namespace JokeApi.Controllers
{
    public class NumbersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
