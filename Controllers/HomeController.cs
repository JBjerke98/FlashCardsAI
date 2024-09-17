using Microsoft.AspNetCore.Mvc;

namespace FlashCardsAI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}