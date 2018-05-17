
using Microsoft.AspNetCore.Mvc;

namespace ProdFeed.Controllers
{
    public class FeedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}