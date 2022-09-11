using Microsoft.AspNetCore.Mvc;

namespace TheMuscleBar.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Status404()
        {
            return View();
        }
    }
}
