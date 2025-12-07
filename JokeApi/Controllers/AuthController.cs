using Microsoft.AspNetCore.Mvc;

namespace JokeApi.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
