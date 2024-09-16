using Microsoft.AspNetCore.Mvc;

namespace FlashCardsAI.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}