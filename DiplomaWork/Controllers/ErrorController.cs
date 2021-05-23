using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaWork1.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        [HttpGet]
        public IActionResult NotFound(string? message = null)
        {
            ViewBag.NotFoundMessage = message;
            return View();
        }

        [HttpGet]
        public IActionResult NotAllowed(string? message = null)
        {
            ViewBag.NotAllowedMessage = message;
            return View();
        }
    }
}
