using Microsoft.AspNetCore.Mvc;


namespace WebAppDECE.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StatusCode(string code)
        {
            return View(code);
        }
    }
}