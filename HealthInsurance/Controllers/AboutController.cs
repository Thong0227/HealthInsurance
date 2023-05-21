using Microsoft.AspNetCore.Mvc;

namespace HealthInsurance.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
